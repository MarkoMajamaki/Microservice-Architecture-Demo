import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class ChangeNotifierWidget<ViewModelType extends ChangeNotifier>
    extends StatefulWidget {
  final Widget Function(BuildContext) builder;
  final Function(ViewModelType)? onModelReady;
  final ViewModelType viewModel;

  const ChangeNotifierWidget({
    Key? key,
    required this.builder,
    this.onModelReady,
    required this.viewModel,
  }) : super(key: key);

  @override
  State<StatefulWidget> createState() => _BaseViewState<ViewModelType>();
}

class _BaseViewState<ViewModelType extends ChangeNotifier>
    extends State<ChangeNotifierWidget<ViewModelType>> {
  late ViewModelType _viewModel;

  @override
  void initState() {
    super.initState();

    _viewModel = widget.viewModel;

    if (widget.onModelReady != null) {
      widget.onModelReady!(_viewModel);
    }
  }

  @override
  Widget build(BuildContext context) {
    return ChangeNotifierProvider(
      create: (context) => _viewModel,
      child: widget.builder(context),
    );
  }
}
