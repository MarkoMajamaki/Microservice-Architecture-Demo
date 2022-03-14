import 'package:flutter/material.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:frontend/core/getit.dart';
import 'package:frontend/views/home/home_view.dart';
import 'package:modulary/modulary.dart';
import 'package:shared/core/dialog_provider.dart';
import 'package:shared/core/localization.dart';
import 'package:shared/core/theme.dart';
import 'package:shared/services/navigation_service.dart';

class App extends StatefulWidget {
  const App({Key? key}) : super(key: key);

  static void setLocale(BuildContext context, Locale newLocale) async {
    _AppState? state = context.findAncestorStateOfType<_AppState>();

    if (state != null) {
      state.changeLanguage(newLocale);
    }
  }

  @override
  State<App> createState() => _AppState();
}

class _AppState extends State<App> {
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
      initialRoute: Home.route,
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
