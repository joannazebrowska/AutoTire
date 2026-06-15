# AutoTire

Aplikacja webowa ułatwiająca kierowcom podjęcie decyzji o sezonowej zmianie opon (letnie/zimowe). System agreguje i analizuje dane historyczne z zewnętrznych serwisów pogodowych, a także pozwala na lokalizację najbliższych warsztatów samochodowych w okolicy użytkownika.

## Technologie
* **Backend:** C# .NET, Web API / REST API
* **Frontend:** JavaScript (ES6), HTML5, CSS3
* **Integracje z zewnętrznymi API:** 
  * OpenWeather API (pobieranie współrzędnych i danych meteorologicznych)
  * Google Places API (lokalizacja warsztatów)
* **Narzędzia:** Git, Visual Studio / VS Code

## Główne Funkcjonalności
* **Hybrydowa Agregacja API (Własne API):** Stworzenie autorskiego endpointu, który przyjmuje nazwę miasta od użytkownika, automatycznie pobiera jego współrzędne geograficzne (Latitude/Longitude), a następnie odpytuje serwis pogodowy o dane historyczne.
* **Algorytm Rekomendacji:** System analizuje średnie temperatury z ostatnich 7 dni i na podstawie trendów pogodowych generuje jasną rekomendację dotyczącą wymiany opon.
* **Geolokalizacja Warsztatów:** Integracja z Google API umożliwiająca wyszukanie i wyświetlenie lokalnych punktów wulkanizacyjnych w wybranym mieście.
