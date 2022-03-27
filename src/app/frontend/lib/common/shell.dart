import 'package:flutter/material.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:frontend/common/getit.dart';
import 'package:modulary/modulary.dart';
import 'package:shared/common/dialog_provider.dart';
import 'package:shared/common/localization.dart';
import 'package:shared/common/theme.dart';
import 'package:shared/services/navigation_service.dart';

class Shell extends StatefulWidget {
  const Shell({Key? key}) : super(key: key);

  static void setLocale(BuildContext context, Locale newLocale) async {
    _ShellState? state = context.findAncestorStateOfType<_ShellState>();

    if (state != null) {
      state.changeLanguage(newLocale);
    }
  }

  @override
  State<Shell> createState() => _ShellState();
}

class _ShellState extends State<Shell> {
  Locale? _locale;

  changeLanguage(Locale locale) {
    setState(() {
      _locale = locale;
    });

    // Temporary quick and dirty hack
    Future.delayed(const Duration(milliseconds: 300), () {
      setState(() {});
    });
  }

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      locale: _locale ?? const Locale("en", "US"),
      debugShowCheckedModeBanner: false,
      theme: getTheme(),
      navigatorKey: getIt<NavigationService>().getNavigatorKey(),
      initialRoute: "/",
      routes: Modules.routes(),
      onGenerateRoute: (settings) => Modules.onGenerateRoute(settings),
      builder: (context, child) => Navigator(
        onGenerateRoute: (settings) => MaterialPageRoute(
          builder: (context) => DialogProvider(
            child: child!,
            routes: Modules.routes(),
            navigationService: getIt<NavigationService>(),
          ),
        ),
      ),
      localizationsDelegates: const [
        Localization.delegate,
        GlobalMaterialLocalizations.delegate,
        GlobalWidgetsLocalizations.delegate,
      ],
      supportedLocales: const [
        Locale("fi", "FI"),
        Locale("en", "US"),
      ],
      localeResolutionCallback: (locale, supportedLocales) {
        for (var supportedLocale in supportedLocales) {
          if (locale != null &&
              supportedLocale.languageCode == locale.languageCode &&
              supportedLocale.countryCode == locale.countryCode) {
            return supportedLocale;
          }
        }
        return supportedLocales.first;
      },
    );
  }
}
