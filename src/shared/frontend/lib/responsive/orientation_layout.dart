import 'package:flutter/material.dart';

class OrientationLayout extends StatelessWidget {
  final Widget Function(BuildContext)? landscape;
  final Widget Function(BuildContext) portrait;
  const OrientationLayout({
    Key? key,
    required this.portrait,
    this.landscape,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    var orientation = MediaQuery.of(context).orientation;
    if (orientation == Orientation.landscape) {
      if (landscape != null) {
        return landscape!(context);
      }
    }

    return portrait(context);
  }
}
