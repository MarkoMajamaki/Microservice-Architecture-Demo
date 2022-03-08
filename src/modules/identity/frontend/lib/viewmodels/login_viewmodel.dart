import 'package:identity/core/getit.dart';
import 'package:identity/models/user.dart';
import 'package:identity/services/email_login_service.dart';
import 'package:identity/services/facebook_login_service.dart';
import 'package:identity/services/google_login_service.dart';
import 'package:shared/services/navigation_service.dart';

class LoginViewModel {
  final NavigationService _navigationService = getIt<NavigationService>();

  ///
  /// Login with Facebook
  ///
  void loginFacebook() async {
    User? user = await FacebookLoginService.login();

    if (user == null) {
      return;
    }
  }

  ///
  /// Login with Google
  ///
  void loginGoogle() async {
    User? user = await GoogleLoginService.login();

    if (user == null) {
      return;
    }
  }

  ///
  /// Login with email
  ///
  void loginWithEmail(String email, String password) async {
    User? user = await EmailLoginService.login(email, password);

    if (user == null) {
      return;
    }
  }

  ///
  /// Register with email
  ///
  void registerWithEmail(String email, String password) async {
    await EmailLoginService.register(email, password);
    User? user = await EmailLoginService.login(email, password);

    if (user == null) {
      return;
    }
  }

  void closeButtonPressed() {
    _navigationService.goBack();
  }
}
