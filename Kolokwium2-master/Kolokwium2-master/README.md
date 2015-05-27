# Kolokwium - zestaw2

## Scenariusz dla projektu Message Publisher

Celem zadania jest przygotowanie aplikacji, która pozwala na symulację sieci społecznościowej opartej na krótkich wpisach tekstowych.  Użytkownik ma możliwość opublikowania tekstu i filtrowania wcześniej wprowadzonych wpisów. Celem przeszukiwania użytkownik wprowadza słowo klucz oraz zakres czasu przeszukiwania. Filtrowanie odbywa się na dwa sposoby: tekst zawierający słowo klucz i tekst opublikowany w zadanym czasie. Dodatkowo użytkownik ma możliwość pokazania wpisów konkretnego użytkownika i zapisania rezultatów filtrowania. Aplikacja jest skonstruowana w oparciu o podejście MVVM.

O ile nie napisano inaczej, cały kod należy napisać w klasie odpowiadającej warstwie ViewModel. Warstwa View jest już zaimplementowana i nie powinny być zmieniana. W ramach implementacji klasy MessagesViewModel należy dostarczyć brakujących składowych (właściwa część zadania). Tam, gdzie mowa o filtrowaniu należy użyć kwerendy LINQ.

Z projektem dostarczone są:
* Klasy pomocnicze:
  * Globals – odpowiedzialna za trzymanie stałych pomocniczych
  * MyDispatcher – odpowiedzialna za wywołanie akcji przekazanej do metody RunOnUi na wątku głównym aplikacji (instancja jest dostarczona pod postacią interfejsu IDispatcher)
  * MyCommand – odpowiedzialna za dostarczenie uproszczonej implementacji dla interfejsu System.Windows.Input.ICommand
* Klasy do implementacji przeszukiwania:
  * MessageSearcher – klasa bazowa dla logiki przeszukiwania
  * TextMessageSearcher – klasa implementująca strategię przeszukiwania dla tekstu zawierających zadany tekst
  * DateMessageSearcher – klasa implementująca strategię przeszukiwania dla wartości dat od-do

### Zadanie 1: Jako użytkownik chcę widzieć ekran ze wszystkimi dostępnymi funkcjami celem przeglądania wpisów obserwowanych użytkowników.

Kryteria oceny:
* Należy zaimplementować interfejs IMessagesViewModel w klasie o nazwie MessagesViewModel.
* Właściwości z metodami get/set wywołują zdarzenie pochodzące z interfejsu INotifyPropertyChanged w momencie przypisania.
* Właściwość ObservedUsers zwraca kolekcję wpisów dla użytkowników.
* Właściwość ObservedUsers może być domyślnie ustawiona do przykładowej listy, która może być wygenerowana za pomocą metody statycznej Globals.GetRandomDataForAllUsers
* Pierwszy element z w/w kolekcji jest aktualnie zalogowanym użytkownikiem, a jako właściciel kolejki jest udostępniony w ramach właściwości UserName.
* Właściwość SelectedUser domyślnie ma wartość aktualnie zalogowanego użytkownika.
* Właściwość ToDate jest ustawiona na dzień dzisiejszy (w momencie uruchomienia aplikacji)
* Właściwość FromDate jest ustawiona na dzień sprzed roku (w momencie uruchomienia aplikacji)

Wskazówki:
* Właściwości typu ICommand mogą być zaimplementowane za pomocą udostępnionej klasy MyCommand.

### Zadanie 2: Jako użytkownik chcę wpisać słowo kluczowe oraz wybrać zakres dat celem przeprowadzenia filtrowania wiadomości wybranego użytkownika.

Kryteria oceny:
* Należy zaimplementować metodą odpowiadającą na wykonanie komendy FilterCommand.
* Filtrowanie odbywa się gdy tekst wprowadzony dla właściwości o nazwie TextFilter, ma wartość różną od null i nie jest pustym tekstem, wybrany użytkownik (SelectedUser) nie jest nullem, podobnie jak lista jego wiadomości nie jest nullem, ani nie jest pusta. 
* Filtrowanie odbywa się przez utworzenie dwóch obiektów DateMessageSearcher i TextMessageSearcher, z przekazanymi parametrami odpowiednio FromDate i ToDate oraz TextFilter. Rezultat wyszukiwania musi być utrwalony we właściwości FilteredMessages. W przypadku braku rezultatów należy pokazać pustą listę.
* Filtrowanie domyślnego zestawu wiadomości powinno odbywać cię przy starcie aplikacji oraz przy zmianie zaznaczonego użytkownika (właściwość SelectedUser).

Wskazówki:
* Porównywanie tekstu do wyszukiwania jest zabiegiem czasochłonnym. Należy mieć to na uwadze implementując rozwiązanie.

### Zadanie 3: Jako użytkownik chcę dodać nową wiadomość do swojej kolejki, celem udostępnienia informacji znajomym.

Kryteria oceny:
* Należy zaimplementować „zapamiętywanie” kolejnych wiadomości w kolejce dla zalogowanego użytkownika (identyfikowanego we właściwości UserName), w ramach metody odpowiadającej na wykonanie komendy SearchCommand. Wiadomość jest konstruowana w następujący sposób: do właściwości Author przypisywany jest zalogowany użytkownik (właściwość UserName), do właściwości Content przypisywana jest wartość właściwości NewMessageText, do właściwości PublishedOn przypisywana jest aktualna data.
* Po dodaniu właściwości uruchamiany jest filtrowanie (patrz Zadanie 2).

### Zadanie 4: Jako użytkownik chcę zapisać na dysku kolekcję odfiltrowanych wiadomości celem późniejszej analizy.

Kryteria oceny:
* Należy zaimplementować metodą odpowiadającą na wykonanie komendy SaveCommand.
* Zawartość kolekcji, w której zapamiętane są odfiltrowane wiadomości (patrz Zadanie 2) jest zapisana w pliku tymczasowym i formacie XML.

Wskazówki:
* Do implementacji zapisu do formatu XML można użyć dodatkowej biblioteki dostarczonej w ramach środowiska .NET.
* Operacje wejścia/wyjścia są zabiegami czasochłonnymi. Należy mieć to na uwadze implementując rozwiązanie.

### Zadanie 5: Należy zmodyfikować strukturę klas wywiedzionych z klasy MessageSearcher, tak aby MessageSearcherbyła klasą abstrakcyjną, a implementacja właściwych metod przeszukiwania była umieszczona tylko w klasach wywiedzionych.
