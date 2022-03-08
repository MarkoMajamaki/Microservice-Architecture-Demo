import 'package:flutter/material.dart';
import 'package:identity/core/getit.dart';
import 'package:identity/viewmodels/login_viewmodel.dart';
import 'package:identity/widgets/facebook_login_button.dart';
import 'package:identity/widgets/google_login_button.dart';
import 'package:shared/core/localization.dart';

enum LoginViewModes { login, signup }

class LoginView extends StatefulWidget {
  static String route = "LoginView";
  const LoginView({Key? key}) : super(key: key);

  @override
  _LoginViewState createState() => _LoginViewState();
}

class _LoginViewState extends State<LoginView> {
  LoginViewModes? _mode;
  final LoginViewModel _viewModel = getIt<LoginViewModel>();

  late TextEditingController _loginEmailTextController;
  late TextEditingController _loginPasswordTextController;

  late TextEditingController _registerEmailTextController;
  late TextEditingController _registerPasswordTextController;

  @override
  void initState() {
    super.initState();
    _loginEmailTextController = TextEditingController();
    _loginPasswordTextController = TextEditingController();
    _registerEmailTextController = TextEditingController();
    _registerPasswordTextController = TextEditingController();
  }

  @override
  Widget build(BuildContext context) {
    _mode ??= ModalRoute.of(context)!.settings.arguments as LoginViewModes;

    return Scaffold(
      appBar: AppBar(
        elevation: 0,
        backgroundColor: Colors.transparent,
        leading: CloseButton(
          onPressed: () {
            _viewModel.closeButtonPressed();
          },
        ),
      ),
      body: Center(
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: ConstrainedBox(
            constraints: const BoxConstraints(maxWidth: 800),
            child: _mode == LoginViewModes.login ? _loginView() : _signinView(),
          ),
        ),
      ),
    );
  }

  ///
  /// Create login view
  ///
  Widget _loginView() {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.center,
      mainAxisAlignment: MainAxisAlignment.center,
      children: [
        Text(
          "login_title".localize(),
          style: _createTitleStyle(),
        ),
        Row(
          crossAxisAlignment: CrossAxisAlignment.center,
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Text(
              "login_no_account".localize(),
              style: _createSubTitleStyle(),
            ),
            TextButton(
              onPressed: () {
                setState(() {
                  _mode = LoginViewModes.signup;
                });
              },
              child: Padding(
                padding: const EdgeInsets.only(left: 8.0),
                child: Text(
                  "login_create_new_account".localize(),
                  style: _createSubTitleStyle(),
                ),
              ),
            ),
          ],
        ),
        Container(height: 40),
        IntrinsicHeight(
          child: Row(
            children: [
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Padding(
                      padding: const EdgeInsets.only(bottom: 4),
                      child: TextField(
                        controller: _loginEmailTextController,
                        decoration:
                            _textFieldDecoration("login_email".localize()),
                      ),
                    ),
                    Padding(
                      padding: const EdgeInsets.only(bottom: 4),
                      child: TextField(
                        controller: _loginPasswordTextController,
                        obscureText: true,
                        decoration:
                            _textFieldDecoration("login_password".localize()),
                      ),
                    ),
                    Align(
                      alignment: Alignment.centerRight,
                      child: Padding(
                        padding: const EdgeInsets.only(bottom: 24, top: 8),
                        child: TextButton(
                          style: _blackTextButtonStyle(),
                          onPressed: () {
                            setState(() {});
                          },
                          child: Text(
                            "login_forget_password".localize(),
                          ),
                        ),
                      ),
                    ),
                    OutlinedButton(
                      onPressed: () {
                        _viewModel.loginWithEmail(
                          _loginEmailTextController.text,
                          _loginPasswordTextController.text,
                        );
                      },
                      child: Padding(
                        padding: const EdgeInsets.all(12.0),
                        child: Text("login_button".localize()),
                      ),
                    ),
                  ],
                ),
              ),
              const VerticalDivider(
                thickness: 1,
                width: 80,
              ),
              Expanded(
                child: Column(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    Padding(
                      padding: const EdgeInsets.only(bottom: 12),
                      child: FacebookLoginButton(
                        text: "login_with_facebook".localize(),
                        pressed: () => _viewModel.loginFacebook(),
                      ),
                    ),
                    Padding(
                      padding: const EdgeInsets.only(bottom: 24),
                      child: GoogleLoginButton(
                        text: "login_with_google".localize(),
                        pressed: () => _viewModel.loginGoogle(),
                      ),
                    ),
                  ],
                ),
              ),
            ],
          ),
        )
      ],
    );
  }

  ///
  /// Create sign in view
  ///
  Widget _signinView() {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.center,
      mainAxisAlignment: MainAxisAlignment.center,
      children: [
        Text(
          "signup_title".localize(),
          style: _createTitleStyle(),
        ),
        Row(
          crossAxisAlignment: CrossAxisAlignment.center,
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Text(
              "signup_already_have_account".localize(),
              style: _createSubTitleStyle(),
            ),
            TextButton(
              onPressed: () {
                setState(() {
                  _mode = LoginViewModes.login;
                });
              },
              child: Padding(
                padding: const EdgeInsets.only(left: 8.0),
                child: Text(
                  "signup_login_with_existing_account".localize(),
                  style: _createSubTitleStyle(),
                ),
              ),
            ),
          ],
        ),
        Container(height: 40),
        IntrinsicHeight(
          child: Row(
            children: [
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Padding(
                      padding: const EdgeInsets.only(bottom: 4),
                      child: TextField(
                        controller: _registerEmailTextController,
                        decoration: _textFieldDecoration(
                            "signup_type_email".localize()),
                      ),
                    ),
                    Padding(
                      padding: const EdgeInsets.only(bottom: 4),
                      child: TextField(
                        decoration: _textFieldDecoration(
                            "signup_type_email_again".localize()),
                      ),
                    ),
                    Padding(
                      padding: const EdgeInsets.only(bottom: 4),
                      child: TextField(
                        controller: _registerPasswordTextController,
                        obscureText: true,
                        decoration: _textFieldDecoration(
                            "signup_type_password".localize()),
                      ),
                    ),
                    Padding(
                      padding: const EdgeInsets.only(bottom: 24),
                      child: TextField(
                        obscureText: true,
                        decoration: _textFieldDecoration(
                            "signup_type_password_again".localize()),
                      ),
                    ),
                    OutlinedButton(
                      onPressed: () {
                        _viewModel.registerWithEmail(
                          _registerEmailTextController.text,
                          _registerPasswordTextController.text,
                        );
                      },
                      child: Padding(
                        padding: const EdgeInsets.all(12.0),
                        child: Text("signup_button".localize()),
                      ),
                    ),
                  ],
                ),
              ),
              const VerticalDivider(
                thickness: 1,
                width: 80,
              ),
              Expanded(
                child: Column(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    Padding(
                      padding: const EdgeInsets.only(bottom: 12),
                      child: FacebookLoginButton(
                        text: "signup_with_facebook".localize(),
                        pressed: () => _viewModel.loginFacebook(),
                      ),
                    ),
                    Padding(
                      padding: const EdgeInsets.only(bottom: 24),
                      child: GoogleLoginButton(
                        text: "signup_with_google".localize(),
                        pressed: () => _viewModel.loginGoogle(),
                      ),
                    ),
                  ],
                ),
              ),
            ],
          ),
        ),
        Padding(
          padding: const EdgeInsets.only(top: 24),
          child: Text(
            "signup_terms_and_conditions".localize(),
            maxLines: 10,
            style: const TextStyle(fontSize: 14),
          ),
        ),
      ],
    );
  }

  ///
  /// Input style
  ///
  InputDecoration _textFieldDecoration(String labelText) {
    return InputDecoration(
      labelText: labelText,
      floatingLabelBehavior: FloatingLabelBehavior.auto,
    );
  }

  ///
  /// Title style
  ///
  TextStyle _createTitleStyle() {
    return const TextStyle(
      fontSize: 40,
      fontWeight: FontWeight.bold,
    );
  }

  ///
  /// Sub title style
  ///
  TextStyle _createSubTitleStyle() {
    return const TextStyle(
      fontSize: 16,
    );
  }

  ButtonStyle _blackTextButtonStyle() {
    return ButtonStyle(
      textStyle: MaterialStateProperty.all(
        const TextStyle(
          fontSize: 15,
        ),
      ),
      foregroundColor: MaterialStateProperty.all(Colors.black87),
    );
  }
}
