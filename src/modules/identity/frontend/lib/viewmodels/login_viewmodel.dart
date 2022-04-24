import 'package:identity/models/user.dart';
import 'package:identity/services/email_login_service.dart';
import 'package:identity/services/facebook_login_service.dart';
import 'package:identity/services/google_login_service.dart';
import 'package:shared/mvvm/view_model_base.dart';
import 'package:shared/services/navigation_service.dart';

enum LoginViewModes { login, signup }

class LoginViewModel extends ViewModelBase {
  LoginViewModel(
    this._navigationService,
    this._facebookLoginService,
    this._googleLoginService,
    this._emailLoginService,
  );

  final NavigationService _navigationService;
  final FacebookLoginService _facebookLoginService;
  final GoogleLoginService _googleLoginService;
  final EmailLoginService _emailLoginService;

  LoginViewModes? mode;

  ///
  /// Login with Facebook
  ///
  void loginFacebook() async {
    User? user = await _facebookLoginService.login();

    if (user == null) {
      return;
    }
  }

  ///
  /// Login with Google
  ///
  void loginGoogle() async {
    User? user = await _googleLoginService.login();

    if (user == null) {
      return;
    }
  }

  ///
  /// Login with email
  ///
  void loginWithEmail(String email, String password) async {
    User? user = await _emailLoginService.login(email, password);

    if (user == null) {
      return;
    }
  }

  ///
  /// Register with email
  ///
  void registerWithEmail(String email, String password) async {
    await _emailLoginService.register(email, password);
    User? user = await _emailLoginService.login(email, password);

    if (user == null) {
      return;
    }
  }

  void closeButtonPressed() {
    _navigationService.goBack();
  }

  void modeChangedCommand(LoginViewModes newMode) {
    mode = newMode;
    notifyListeners();
  }
}
