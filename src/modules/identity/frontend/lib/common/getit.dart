import 'package:get_it/get_it.dart';
import 'package:identity/viewmodels/login_viewmodel.dart';

GetIt getIt = GetIt.instance;

void initializeGetIt() {
  getIt.registerFactory<LoginViewModel>(() => LoginViewModel());
}
