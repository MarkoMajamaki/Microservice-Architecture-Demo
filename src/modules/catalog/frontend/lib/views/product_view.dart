import 'package:catalog/common/getit.dart';
import 'package:catalog/viewmodels/product_viewmodel.dart';
import 'package:flutter/material.dart';
import 'package:shared/mvvm/view_base.dart';

class ProductView extends StatelessWidget {
  static String route = "ProductView";

  const ProductView({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return MVVM<ProductViewModel>.builder(
      viewModel: getIt<ProductViewModel>(),
      view: (viewModel) {
        return Scaffold(
          body: Center(
              child: Column(
            children: [
              Text(
                viewModel.title,
              ),
              ElevatedButton(
                onPressed: () {
                  viewModel.updateTitle();
                },
                child: const Text("Increase counter"),
              ),
              ElevatedButton(
                onPressed: () {
                  viewModel.navigateCommand();
                },
                child: const Text("navigate"),
              ),
              ElevatedButton(
                onPressed: () {
                  viewModel.goBackCommand();
                },
                child: const Text("go back"),
              )
            ],
          )),
        );
      },
    );
  }
}
