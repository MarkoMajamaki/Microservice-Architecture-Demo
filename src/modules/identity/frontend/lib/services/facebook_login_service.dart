import 'dart:async';
import 'dart:convert';
import 'dart:io';
import 'package:flutter_facebook_auth/flutter_facebook_auth.dart';
import 'package:http/http.dart';
import 'package:identity/core/api.dart';
import 'package:identity/models/user.dart';
import 'package:shared/models/token.dart';

class FacebookLoginService {
  ///
  /// Login with Facebook authentication
  ///
  static Future<User?> login() async {
    final LoginResult result = await FacebookAuth.instance.login(
      loginBehavior: LoginBehavior.webOnly,
    );

    if (result.status == LoginStatus.success) {
      final AccessToken accessToken = result.accessToken!;

      final request = await post(
        Uri.parse(loginFacebookUrl),
        headers: {
          HttpHeaders.contentTypeHeader: "application/json",
          HttpHeaders.acceptHeader: "application/json",
        },
        body: json.encode(
          {
            "Token": accessToken.token,
          },
        ),
      );

      if (request.statusCode != 200) {
        throw request.reasonPhrase ?? "";
      }

      // Our system access token
      Token token = Token.fromJson(jsonDecode(request.body));

      // Get facebook user data
      final userData = await FacebookAuth.instance.getUserData();
      _FacebookUser facebookUser = _FacebookUser.fromMap(userData);

      return User(
        id: token.userId,
        email: facebookUser.email,
        userName: facebookUser.name,
        pictureUri: facebookUser.picture != null
            ? facebookUser.picture!.data.url
            : null,
      );
    } else {
      throw "Login failed!";
    }
  }

  ///
  /// Logout from Facebook auth
  ///
  static Future logout() async {
    await FacebookAuth.instance.logOut();
  }
}

class _FacebookUser {
  final String id;
  final String name;
  final String email;
  final _Picture? picture;

  _FacebookUser.fromMap(Map<String, dynamic> json)
      : id = json['id'],
        name = json["name"],
        email = json["email"],
        picture = json.containsKey("picture")
            ? _Picture.fromMap(Map<String, dynamic>.from(json["picture"]))
            : null;
}

class _Picture {
  final _Data data;
  _Picture.fromMap(Map<String, dynamic> json)
      : data = _Data.fromMap(Map<String, dynamic>.from(json["data"]));
}

class _Data {
  final int height;
  final int width;
  final String url;
  final bool isSilhouette;

  _Data.fromMap(Map<String, dynamic> json)
      : height = json["height"],
        width = json["width"],
        url = json["url"],
        isSilhouette = json["is_silhouette"];
}
