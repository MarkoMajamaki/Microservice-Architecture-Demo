import 'package:flutter/material.dart';
import 'package:frontend/core/app.dart';
import 'package:frontend/core/configure_nonweb.dart'
    if (dart.library.html) 'package:frontend/core/configure_web.dart';
import 'package:frontend/module.dart';
import 'package:modulary/modulary.dart';

void main() {
  configureApp();

  Modules.initialize([
    MainModule(),
  ]);

  runApp(const App());
}
