import 'package:get_it/get_it.dart';
import 'package:identity/services/email_login_service.dart';
import 'package:identity/services/facebook_login_service.dart';
import 'package:identity/services/google_login_service.dart';
import 'package:identity/viewmodels/login_viewmodel.dart';
import 'package:shared/services/navigation_service.dart';

GetIt getIt = GetIt.instance;

void initializeGetIt() {
  getIt.registerFactory<LoginViewModel>(
    () => LoginViewModel(
      getIt<NavigationService>(),
      FacebookLoginService(),
      GoogleLoginService(),
      EmailLoginService(),
    ),
  );
}
