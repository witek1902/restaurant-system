# System zarządzania zamówieniami w restauracji

![Food_Portal](https://witek1902.github.io/restaurant-system/img/food_portal_main.png)

## Film obrazujący działanie aplikacje 
https://vimeo.com/154968913

## Opis funkcjonalności
https://witek1902.github.io/restaurant-system/

## Jak uruchomić lokalnie

1. Stwórz bazę danych o nazwie **OMSDb**.
2. Podmień ConnectionString w pliku **Web.config**.
3. Uruchom aplikacje (dzięki temu stworzone zostaną testowe konta, do których zostaną przypisane testowe dane).
4. Uruchom skrypt **dbo.CreateDatabase**.
5. Jeśli interesują Cię testowe dane (kilka przykładowych restauracji z uzupełnionym menu) uruchom skrypt **InitialDatabase**.

## Testowe konta do obsługi zamówień
```
Login/password
customer/customer (przykładowe konto klienta)

rw-waiter/rw-waiter (konto kelnera w Restuaracja u Witka)
rw-manager/rw-manager (konto managera w Restuaracja u Witka)
rw-cook/rw-cook (konto kucharza w Restuaracja u Witka)
```

Analogicznie stworzone zostały konta dla innych przykładowych restauracji (dodanych po uruchomieniu skryptu **InitialDatabase**:
- prefix **rf** dla Restuaracja francuska,
- prefix **rt** dla Restauracja tajska,
- prefix **rs** dla Restuaracja staropolska

## Dodatkowe informacje
- W klasie **InitializeSimpleMembershipAttribute** tworzone są testowe konta z różnymi rolami (kelner, manager, klient).
