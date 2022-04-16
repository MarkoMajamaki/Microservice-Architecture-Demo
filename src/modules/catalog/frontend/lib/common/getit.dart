import 'package:catalog/viewmodels/catalog_viewmodel.dart';
import 'package:catalog/viewmodels/product_viewmodel.dart';
import 'package:get_it/get_it.dart';
import 'package:shared/services/navigation_service.dart';

GetIt getIt = GetIt.instance;

void initializeGetIt() {
  getIt.registerFactory<CatalogViewModel>(
      () => CatalogViewModel(getIt<NavigationService>()));

  getIt.registerFactory<ProductViewModel>(
      () => ProductViewModel(getIt<NavigationService>()));
}
