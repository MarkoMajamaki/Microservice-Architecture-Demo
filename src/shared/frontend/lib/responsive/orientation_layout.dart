import 'package:flutter/material.dart';

class OrientationLayout extends StatelessWidget {
  final Widget landscape;
  final Widget portrait;
  const OrientationLayout({
    Key? key,
    required this.portrait,
    required this.landscape,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    var orientation = MediaQuery.of(context).orientation;
    if (orientation == Orientation.landscape) {
      return landscape;
    }

    return portrait;
  }
}
