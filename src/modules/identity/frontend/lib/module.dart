import 'package:flutter/foundation.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_facebook_auth/flutter_facebook_auth.dart';
import 'package:identity/views/login_view.dart';
import 'package:shared/core/localization.dart';
import 'package:modulary/modulary.dart';

import 'core/getit.dart';

class AuthModule extends Module {
  @override
  void initialize() {
    // Add module localization strings source folder
    Localization.addSource("packages/identity/lang");

    if (kIsWeb) {
      // initialiaze the facebook javascript SDK
      FacebookAuth.instance.webInitialize(
        appId: "129001999236805",
        cookie: true,
        xfbml: true,
        version: "v11.0",
      );
    }
    // Init dependency injection objects
    initializeGetIt();
  }

  ///
  /// Get module routes
  ///
  @override
  Map<String, Widget Function(BuildContext p1)> get routes {
    return {
      // LoginView.route: (context) => LoginView(),
    };
  }

  @override
  Route<dynamic>? onGenerateRoute(RouteSettings settings) {
    if (settings.name == LoginView.route) {
      return PageRouteBuilder(
        settings: settings,
        pageBuilder: (_, __, ___) => const LoginView(),
        transitionsBuilder: (_, a, __, c) =>
            FadeTransition(opacity: a, child: c),
      );
    }
  }
}
