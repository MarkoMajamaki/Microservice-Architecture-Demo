import 'dart:convert';
import 'dart:io';
import 'package:google_sign_in/google_sign_in.dart';
import 'package:http/http.dart';
import 'package:identity/common/api.dart';
import 'package:identity/models/user.dart';
import 'package:shared/models/token.dart';

class GoogleLoginService {
  static final GoogleSignIn _googleSignIn = GoogleSignIn();

  ///
  /// Login with Google authentication
  ///
  static Future<User?> login() async {
    GoogleSignInAccount? account = await _googleSignIn.signIn();

    if (account == null) {
      throw "Login failed!";
    }

    GoogleSignInAuthentication auth = await account.authentication;

    final request = await post(
      Uri.parse(loginGoogleUrl),
      headers: {
        HttpHeaders.contentTypeHeader: "application/json",
        HttpHeaders.acceptHeader: "application/json",
      },
      body: json.encode(
        {
          "Token": "${auth.idToken}",
        },
      ),
    );

    if (request.statusCode != 200) {
      throw request.reasonPhrase ?? "";
    }

    // Our system access token
    Token token = Token.fromJson(jsonDecode(request.body));

    return User(
      id: token.userId,
      userName: account.displayName,
      email: account.email,
      pictureUri: account.photoUrl,
    );
  }

  ///
  /// Logout
  ///
  static Future logOut() async {
    return await _googleSignIn.disconnect();
  }
}
