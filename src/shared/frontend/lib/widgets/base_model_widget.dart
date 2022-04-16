// import 'package:flutter/material.dart';
// import 'package:provider/provider.dart';

// abstract class BaseViewWidget<T> extends Widget {
//   const BaseViewWidget({Key? key}) : super(key: key);

//   @protected
//   Widget build(BuildContext context, T model);

//   @override
//   _DataProviderElement<T> createElement() => _DataProviderElement<T>(this);
// }

// class _DataProviderElement<T> extends ComponentElement {
//   _DataProviderElement(BaseViewWidget widget) : super(widget);

//   @override
//   BaseViewWidget get widget => super.widget;

//   // When executing the above build method, we pass back the model we get from Provider.
//   @override
//   Widget build() => widget.build(this, Provider.of<T>(this));
// }
