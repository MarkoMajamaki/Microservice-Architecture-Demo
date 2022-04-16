import 'package:flutter/foundation.dart';
import 'package:shared/mvvm/view_model_base.dart';
import 'package:shared/services/navigation_service.dart';

class ProductViewModel extends ViewModelBase {
  final NavigationService navigationService;

  String title = "ProductViewModel";
  int counter = 0;

  ProductViewModel(this.navigationService);

  @override
  void init() {
    title = "initialized: $title";
    notifyListeners();
  }

  void updateTitle() {
    counter++;
    title = "updated: $counter";
    notifyListeners();
  }

  @override
  void onBuild() {
    if (kDebugMode) {
      print("onBuild: $title");
    }
  }

  @override
  void onPopNext() {
    if (kDebugMode) {
      print("onPopNext: $title");
    }
  }

  @override
  void onPush() {
    if (kDebugMode) {
      print("onPush: $title");
    }
  }

  @override
  void onPop() {
    if (kDebugMode) {
      print("onPush: $title");
    }
  }

  @override
  void onPushNext() {
    if (kDebugMode) {
      print("onPushNext: $title");
    }
  }

  void navigateCommand() {
    navigationService.navigate("CatalogView");
  }

  void goBackCommand() {
    navigationService.goBack();
  }
}
