import 'dart:io';
import 'package:flutter/foundation.dart';

enum Enviroments { croduction, development }

///
/// Current app enviroment
///
const Enviroments enviroment = Enviroments.development;

///
/// API base address based on current enviroment
///
String get baseUrl {
  // Jos K8S nii tällöin api-auth!
  if (kIsWeb) {
    return "https://localhost:8000";
  } else if (Platform.isAndroid) {
    return "https://10.0.2.2:8000";
  } else {
    return "https://localhost:8000";
  }
}
