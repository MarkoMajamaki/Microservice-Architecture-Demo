import 'package:flutter/foundation.dart';
import 'package:shared/mvvm/view_model_base.dart';
import 'package:shared/services/navigation_service.dart';

class CatalogViewModel extends ViewModelBase {
  final NavigationService navigationService;

  String title = "CatalogViewModel";
  int counter = 0;

  CatalogViewModel(this.navigationService);

  @override
  void init() {
    title = "initialized $title";
    notifyListeners();
  }

  void updateTitle() {
    counter++;
    title = "updated: $counter";
    notifyListeners();
  }

  void navigateCommand() {
    navigationService.navigate("ProductView");
  }

  void goBackCommand() {
    navigationService.goBack();
  }
}
