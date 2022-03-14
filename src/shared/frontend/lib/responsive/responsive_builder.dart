import 'package:flutter/material.dart';
import 'package:shared/responsive/device_screen_type.dart';
import 'package:shared/responsive/sizing_information.dart';

class ResponsiveBuilder extends StatelessWidget {
  const ResponsiveBuilder({Key? key, required this.builder}) : super(key: key);

  final Widget Function(
      BuildContext context, SizingInformation sizingInformation) builder;

  @override
  Widget build(BuildContext context) {
    var mediaQuery = MediaQuery.of(context);

    return LayoutBuilder(builder: (context, boxSizing) {
      var sizingInformation = SizingInformation(
        orientation: mediaQuery.orientation,
        deviceType: getDeviceType(mediaQuery),
        screenSize: mediaQuery.size,
        localWidgetSize: Size(boxSizing.maxWidth, boxSizing.maxHeight),
      );

      return builder(context, sizingInformation);
    });
  }

  DeviceScreenType getDeviceType(MediaQueryData mediaQuery) {
    var orientation = mediaQuery.orientation;

    double deviceWidth = 0;

    if (orientation == Orientation.landscape) {
      deviceWidth = mediaQuery.size.height;
    } else {
      deviceWidth = mediaQuery.size.width;
    }

    if (deviceWidth > 950) {
      return DeviceScreenType.desktop;
    }

    if (deviceWidth > 600) {
      return DeviceScreenType.tablet;
    }

    return DeviceScreenType.mobile;
  }
}
