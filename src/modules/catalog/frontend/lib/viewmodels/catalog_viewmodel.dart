import 'package:flutter/material.dart';

class CatalogViewModel extends ChangeNotifier {
  String title = "start";
  int counter = 0;

  void initialize() {
    title = "initialized";
    notifyListeners();
  }

  void updateTitle() {
    counter++;
    title = "updated: $counter";
    notifyListeners();
  }
}
