import 'package:flutter/material.dart';

abstract class ViewModelBase extends ChangeNotifier {
  ViewModelBase();

  ///
  /// Called once when view is initialized
  ///
  void init() {}

  ///
  /// Called every time when view is rebuilt
  ///
  void onBuild() {}

  ///
  /// View activated
  ///
  void activate() {}

  ///
  /// View deactivated
  ///
  void deactivate() {}
}
