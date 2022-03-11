import 'package:flutter/material.dart';
import 'package:shared/services/navigation_service.dart';

class DialogProvider extends StatefulWidget {
  final Widget child;
  final Map<String, WidgetBuilder> routes;
  final NavigationService navigationService;

  const DialogProvider(
      {Key? key,
      required this.child,
      required this.routes,
      required this.navigationService})
      : super(key: key);

  @override
  _DialogProviderState createState() => _DialogProviderState();
}

class _DialogProviderState extends State<DialogProvider> {
  @override
  void initState() {
    super.initState();
    widget.navigationService.registerDialogProvider(openDialog, closeDialog);
  }

  @override
  Widget build(BuildContext context) {
    return widget.child;
  }

  ///
  /// Show dialog
  ///
  Future<dynamic> openDialog(String route, dynamic arguments) async {
    if (widget.routes.containsKey(route) == false) {
      throw Exception("Dialog route not found!");
    }

    return await showDialog(
      context: context,
      barrierDismissible: true,
      builder: (context) => widget.routes[route]!(context),
    );
  }

  ///
  /// Close dialog if open
  ///
  void closeDialog({dynamic response}) {
    Navigator.of(context, rootNavigator: true).pop(response);
  }
}
