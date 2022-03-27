import 'package:get_it/get_it.dart';
import 'package:shared/services/navigation_service.dart';

GetIt getIt = GetIt.instance;

///
/// Initialize service locator
///
void initializeGetIt() {
  // Top navigation service
  getIt.registerLazySingleton(() => NavigationService());
}
