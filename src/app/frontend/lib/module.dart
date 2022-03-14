import 'package:flutter/material.dart';
import 'package:frontend/core/getit.dart';
import 'package:frontend/views/home/home_view.dart';
import 'package:modulary/modulary.dart';

class MainModule extends Module {
  @override
  void initialize() {
    // Add module localization strings source folder
    // Localization.addSource("lang");

    // Init dependency injection objects
    initializeGetIt();
  }

  @override
  Map<String, WidgetBuilder> get routes {
    return {
      Home.route: (context) => const Home(),
    };
  }

  ///
  /// Generate route
  ///
  @override
  Route<dynamic>? onGenerateRoute(RouteSettings settings) {
    return null;
  }
}
