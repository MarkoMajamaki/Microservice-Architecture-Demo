import 'package:flutter/material.dart';

class FacebookLoginButton extends StatefulWidget {
  final String text;
  final Function pressed;

  const FacebookLoginButton({
    Key? key,
    required this.text,
    required this.pressed,
  }) : super(key: key);

  @override
  _FacebookLoginButtonState createState() => _FacebookLoginButtonState();
}

class _FacebookLoginButtonState extends State<FacebookLoginButton> {
  @override
  Widget build(BuildContext context) {
    return ElevatedButton(
      onPressed: () => widget.pressed(),
      child: Padding(
        padding: const EdgeInsets.all(12.0),
        child: Stack(
          children: [
            const Align(
              alignment: Alignment.centerLeft,
              child: SizedBox(
                width: 24,
                height: 24,
                child: Image(
                  image: AssetImage("images/facebook_logo.png"),
                ),
              ),
            ),
            Positioned.fill(
              child: Align(
                alignment: Alignment.center,
                child: Text(
                  widget.text,
                ),
              ),
            ),
          ],
        ),
      ),
      style: ButtonStyle(
        elevation: MaterialStateProperty.all(0),
        backgroundColor:
            MaterialStateProperty.all(const Color.fromRGBO(71, 119, 238, 1.0)),
      ),
    );
  }
}
