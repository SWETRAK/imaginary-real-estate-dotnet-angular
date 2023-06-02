# Imaginary real estate

Portal ogłoszeń z nieruchomościami powstały na potrzeby zaliczenia przedmiotu ```Szkielety programistyczne w aplikacjach internetowych```.

## Uruchamianie backend

Do odpalenia potrzebne jest sdk .NET Core 6.0 dostępne na stronie [DOTNET DOWNLOAD](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

Następnym krokiem jest uruchomienie servera bazy danych MongoDB na porcie 27017 bez domyślnego urzytkownika.

Kolejnym krokiem jest uruchomineie servera usługi MinIO. Aby to zrobic mozna skozystac z oprogramowania w docker i komendy ```docker run -n minio -p 9000:9000 -p 9001:9001 -e MINIO_ROOT_USER="minio" -e MINIO_ROOT_PASSWORD="minio123" bitnami/minio:latest```. Następnym krokiem jest przejscie do panelu administratora, zalogownaie się za pomocą login: ```minio```, password: ```minio123```. Prejscie do zakładki Access Token i dodadnie nowego Access key: ```KkVHcZCMVf8vSqdQ``` and secret key: ```kYRv1SJ8CWiONFACnpKQjRUEMFePt5Oz``` oraz utworzenie bucketu o nazwie ```project-octopus```. 

Nastęnie z poziomu folderu głównego projektu nalerzy wyda polecenie w konsoli ```dotnet run --project ./server/ImaginaryRealEstate/ImaginaryRealEstate/ImaginaryRealEstate.csproj```.


## Uruchamianie frontend

w terminalu w folderze ```./client``` wydac komendę ```npm install``` i ```npm run ng serve```.

