import 'package:flutter/material.dart';

class TabNavigationService {
  final List<GlobalKey<NavigatorState>> _mainTabNavigatorKeys = [];

  ///
  /// Selected main tab
  ///
  int selectedTabIndex = 0;

  TabNavigationService(int tabCount) {
    for (int i = 0; i < tabCount; i++) {
      _mainTabNavigatorKeys.add(GlobalKey<NavigatorState>());
    }
  }

  ///
  /// Navigate to new screen
  ///
  Future<dynamic> navigate(String routeName, {dynamic arguments}) {
    return _mainTabNavigatorKeys[selectedTabIndex]
        .currentState!
        .pushNamed(routeName, arguments: arguments);
  }

  ///
  /// Navigate to previous screen
  ///
  void goBack({dynamic arguments}) {
    return _mainTabNavigatorKeys[selectedTabIndex].currentState!.pop(arguments);
  }

  ///
  /// Could navigate back
  ///
  Future<bool> canGoBack() {
    return _mainTabNavigatorKeys[selectedTabIndex].currentState!.maybePop();
  }

  ///
  /// Get navigation key for main tab by index
  ///
  GlobalKey<NavigatorState> getNavigationKey(int mainTabIndex) {
    return _mainTabNavigatorKeys[mainTabIndex];
  }
}
