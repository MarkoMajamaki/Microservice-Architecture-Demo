import 'package:flutter/material.dart';
import 'package:shared/mvvm/view_model_base.dart';
import 'package:provider/provider.dart';

///
/// MVVM view
///
class MVVM<T extends ViewModelBase> extends StatefulWidget {
  final Widget Function(T) view;
  final T viewModel;

  const MVVM.builder({
    Key? key,
    required this.viewModel,
    required this.view,
  }) : super(key: key);

  @override
  _MVVMState<T> createState() => _MVVMState<T>();
}

///
/// MVVM view base state
///
class _MVVMState<T extends ViewModelBase> extends State<MVVM<T>> {
  ///
  /// Build view
  ///
  @override
  Widget build(BuildContext context) {
    widget.viewModel.onBuild();

    return ChangeNotifierProvider(
      create: (BuildContext context) => widget.viewModel,
      child: Consumer<T>(
        builder: (context, viewModel, child) => widget.view(viewModel),
      ),
    );
  }

  @mustCallSuper
  @override
  void initState() {
    super.initState();
    widget.viewModel.init();
  }

  @mustCallSuper
  @override
  void activate() {
    super.activate();
    widget.viewModel.activate();
  }

  @mustCallSuper
  @override
  void deactivate() {
    super.deactivate();
    widget.viewModel.deactivate();
  }
}
