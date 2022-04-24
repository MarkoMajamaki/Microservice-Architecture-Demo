import 'package:catalog/module.dart';
import 'package:flutter/material.dart';
import 'package:frontend/common/shell.dart';
import 'package:frontend/common/configure_nonweb.dart'
    if (dart.library.html) 'package:frontend/common/configure_web.dart';
import 'package:frontend/module.dart';
import 'package:identity/module.dart';
import 'package:modulary/modulary.dart';

void main() {
  configureApp();

  Modules.initialize([
    MainModule(),
    CatalogModule(),
    IdentityModule(),
  ]);

  runApp(const Shell());
}
