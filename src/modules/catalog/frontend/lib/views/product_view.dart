import 'package:flutter/material.dart';

class ProductView extends StatefulWidget {
  const ProductView({Key? key}) : super(key: key);

  static String route = "product";

  @override
  State<ProductView> createState() => _ProductViewState();
}

class _ProductViewState extends State<ProductView> {
  @override
  Widget build(BuildContext context) {
    return const Scaffold(
      body: Center(
        child: Text("ProductView"),
      ),
    );
  }
}
