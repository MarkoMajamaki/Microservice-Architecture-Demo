import 'package:catalog/common/getit.dart';
import 'package:catalog/views/catalog_view.dart';
import 'package:catalog/views/product_view.dart';
import 'package:flutter/material.dart';
import 'package:modulary/modulary.dart';

class CatalogModule extends Module {
  @override
  void initialize() {
    initializeGetIt();
  }

  ///
  /// Get module routes
  ///
  @override
  Map<String, Widget Function(BuildContext p1)> get routes {
    return {
      "/": (context) => const CatalogView(),
      ProductView.route: (context) => const ProductView(),
    };
  }
}
