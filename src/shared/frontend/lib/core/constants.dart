import 'package:flutter/material.dart';

const double mobileClientTreshold = 600;

///
/// Check is mobile layout used
///
bool isMobileClient(BuildContext context) {
  return MediaQuery.of(context).size.shortestSide < mobileClientTreshold;
}
