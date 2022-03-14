import 'package:flutter/material.dart';
import 'package:frontend/core/getit.dart';
import 'package:shared/core/constants.dart';
import 'package:shared/services/navigation_service.dart';

///
/// Home widget to build tab buttons on bottom or left sidebar and content
///
class Home extends StatefulWidget {
  static String route = "home";
  const Home({Key? key}) : super(key: key);

  @override
  _HomeState createState() => _HomeState();
}

class _HomeState extends State<Home> {
  // Set navigation service selectedMainTabIndex
  final NavigationService _navigationService = getIt<NavigationService>();

  ///
  /// Build Home
  ///
  @override
  Widget build(BuildContext context) {
    return WillPopScope(
      // let system handle back button if we're on the first route
      onWillPop: () async => !await _navigationService.canGoBack(),
      child: (isMobileClient(context)) ? _mobileHome() : _desktopHome(),
    );
  }

  ///
  /// Create main view frontend content with bottom tab navigation
  ///
  Widget _mobileHome() {
    return Scaffold(
      body: _content(),
    );
  }

  ///
  /// Create view desktop content with left tabs
  ///
  Widget _desktopHome() {
    return Scaffold(
      body: _content(),
    );
  }

  ///
  /// Create tab root content
  ///
  Widget _content() {
    return const Placeholder();
  }
}
