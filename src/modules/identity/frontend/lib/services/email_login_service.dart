import 'dart:convert';
import 'dart:io';
import 'package:http/http.dart';
import 'package:identity/common/api.dart';
import 'package:identity/models/user.dart';
import 'package:shared/models/token.dart';

class EmailLoginService {
  ///
  /// Register new user
  ///
  Future register(String email, String password) async {
    final request = await post(
      Uri.parse(registerEmailUrl),
      headers: {
        HttpHeaders.contentTypeHeader: "application/json",
        HttpHeaders.acceptHeader: "application/json",
      },
      body: json.encode(
        {
          "Email": email,
          "Password": password,
        },
      ),
    );

    if (request.statusCode != 200) {
      throw request.reasonPhrase ?? "";
    }
  }

  ///
  /// Login with email
  ///
  Future<User?> login(String email, String password) async {
    final request = await post(
      Uri.parse(loginEmailUrl),
      headers: {
        HttpHeaders.contentTypeHeader: "application/json",
        HttpHeaders.acceptHeader: "application/json",
      },
      body: json.encode(
        {
          "Email": email,
          "Password": password,
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
      email: email,
      token: token,
    );
  }

  ///
  /// Logout
  ///
  Future logout() {
    throw "Not implemented!";
  }
}
