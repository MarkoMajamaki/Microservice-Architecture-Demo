import 'dart:async';
import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class Localization {
  final Locale locale;

  Localization(this.locale);

  ///
  /// Localization JSON files directory paths
  ///
  static final List<String> _sources = [];

  ///
  /// Add localization JSON file content provider
  ///
  static void addSource(String path) {
    _sources.add(path);
  }

  ///
  /// Static member to have a simple access to the delegate from the MaterialApp
  ///
  static const LocalizationsDelegate<Localization> delegate =
      _LocalizationDelegate();

  static Localization? get instance => _LocalizationDelegate.instance;

  ///
  /// Helper method to keep the code in the widgets concise Localizations are
  /// accessed using an InheritedWidget "of" syntax
  ///
  static Localization? of(BuildContext context) {
    return Localizations.of<Localization>(context, Localization);
  }

  Map<String, String>? _localizedStrings;

  ///
  /// Cache the language JSON file from the "lang" folder
  ///
  Future<bool> load() async {
    var jsonMap = <String, dynamic>{};

    await Future.forEach(_sources, (String path) async {
      // Get localizaed strings key vaule pairs from correct language file
      String jsonString =
          await rootBundle.loadString("$path/${locale.languageCode}.json");

      // Collect localized strings to map
      jsonMap.addAll(json.decode(jsonString));
    });

    // Save all localized key value pairs to local variable
    _localizedStrings = jsonMap.map((key, value) {
      return MapEntry(key, value.toString());
    });

    return true;
  }

  ///
  /// This method will be called from every widget which needs a localized text
  ///
  String translate(String key) {
    if (_localizedStrings == null) {
      throw Exception("Language JSON files load is not ready or started!");
    }

    String? translate = _localizedStrings![key];

    if (translate == null) {
      throw Exception("Translate for key ($key) not found");
    } else {
      return translate;
    }
  }
}

///
/// LocalizationsDelegate is a factory for a set of localized resources
/// In this case, the localized strings will be gotten in an AppLocalizations object
///
class _LocalizationDelegate extends LocalizationsDelegate<Localization> {
  // This delegate instance will never change (it doesn't even have fields!)
  // It can provide a constant constructor.
  const _LocalizationDelegate();

  static Localization? instance;

  @override
  bool isSupported(Locale locale) {
    // Include all of your supported language codes here
    return [
      'en',
      'fi',
    ].contains(locale.languageCode);
  }

  @override
  Future<Localization> load(Locale locale) async {
    // Loc class is where the JSON loading actually runs
    Localization localizations = Localization(locale);
    await localizations.load();
    instance = localizations;
    return localizations;
  }

  @override
  bool shouldReload(_LocalizationDelegate old) => false;
}

///
/// Extension for string localizer
///
extension StringExtension on String {
  String localize() {
    return Localization.instance!.translate(this);
  }
}
