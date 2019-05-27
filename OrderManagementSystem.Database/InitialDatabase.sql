use OMSDb

-- Dodawanie nowych restauracji
insert into dbo.Restaurant
(RestaurantId, Name, UniqueCode, City, Street, PostalCode, StreetNumber, FlatNumber, ManagerId, PhotoUrl)
VALUES
(newid(), 'Restauracja u Witka', 'RW-WAW01', 'Warszawa', 'Kubańska' ,'03-949', 8, 34, NULL, 'https://witek1902.github.io/restaurant-system/app-examples/restauracja.jpg'),
(newid(), 'Restauracja francuska', 'RF-WAW02', 'Warszawa', 'Plac Piłsudzkiego', '00-321', 1,1, NULL, 'https://witek1902.github.io/restaurant-system/app-examples/restauracjafrancuska.jpg'),
(newid(), 'Restauracja tajska', 'RT-WAW03', 'Warszawa', 'Saska Kępa', '03-421', 43, 1, NULL, 'https://witek1902.github.io/restaurant-system/app-examples/restauracjatajska.jpg'),
(newid(), 'Restauracja staropolska', 'RS-WAW04', 'Warszawa', 'Umińskiego', '32-123', 54, 12, NULL, 'https://witek1902.github.io/restaurant-system/app-examples/restauracjastaropolska.jpg')

declare @restauracjaWitka uniqueidentifier
select @restauracjaWitka = RestaurantId from dbo.Restaurant where Name = 'Restauracja u Witka'
declare @restauracjaFrancuska uniqueidentifier
select @restauracjaFrancuska = RestaurantId from dbo.Restaurant where Name = 'Restauracja francuska'
declare @restauracjaTajska uniqueidentifier
select @restauracjaTajska = RestaurantId from dbo.Restaurant where Name = 'Restauracja tajska'
declare @restauracjaStaropolska uniqueidentifier
select @restauracjaStaropolska = RestaurantId from dbo.Restaurant where Name = 'Restauracja staropolska'

-- Pobieranie appUserów
declare @rwManager int
select @rwManager = UserId from dbo.AppUser where [Login] = 'rw-manager'
declare @rfManager int
select @rfManager = UserId from dbo.AppUser where [Login] = 'rf-manager'
declare @rtManager int
select @rtManager = UserId from dbo.AppUser where [Login] = 'rt-manager'
declare @rsManager int
select @rsManager = UserId from dbo.AppUser where [Login] = 'rs-manager'
declare @rwWaiter int
select @rwWaiter = UserId from dbo.AppUser where [Login] = 'rw-waiter'
declare @rfWaiter int
select @rfWaiter = UserId from dbo.AppUser where [Login] = 'rf-waiter'
declare @rtWaiter int
select @rtWaiter = UserId from dbo.AppUser where [Login] = 'rt-waiter'
declare @rsWaiter int
select @rsWaiter = UserId from dbo.AppUser where [Login] = 'rs-waiter'
declare @rwCook int
select @rwCook = UserId from dbo.AppUser where [Login] = 'rw-Cook'
declare @rfCook int
select @rfCook = UserId from dbo.AppUser where [Login] = 'rf-Cook'
declare @rtCook int
select @rtCook = UserId from dbo.AppUser where [Login] = 'rt-Cook'
declare @rsCook int
select @rsCook = UserId from dbo.AppUser where [Login] = 'rs-Cook'

-- Dodawanie pracowników restauracji
-- Pozycje:
-- 1 Waiter
-- 2 Cook
-- 3 Manager
insert into dbo.RestaurantWorker
(RestaurantWorkerId, Firstname, Lastname, Nick, PositionId, RestaurantId, UserId)
values
-- Employees 'Restauracja u Witka'
(newid(), 'Robert', 'Witkowski', 'Witek', 3, @restauracjaWitka, @rwManager),
(newid(), 'Mariusz', 'Strzelczyk', 'Strzała', 1, @restauracjaWitka, @rwWaiter),
(newid(), 'Arkadiusz', 'Kowalski', 'Kowal', 2, @restauracjaWitka, @rwCook),
-- Employees 'Restauracja francuska'
(newid(), 'Justyna', 'Szymborska', 'Justyś', 3, @restauracjaFrancuska, @rfManager),
(newid(), 'Ramona', 'Konopka', 'Ramcia', 1, @restauracjaFrancuska, @rfWaiter),
(newid(), 'Katarzyna', 'Galik', 'Gali', 2, @restauracjaFrancuska, @rfCook),
-- Employees 'Restauracja tajska'
(newid(), 'Maurico', 'Peamo', 'Peamo', 3, @restauracjaTajska, @rtManager),
(newid(), 'Marono', 'Michelle', 'Michellini', 1, @restauracjaTajska, @rtWaiter),
(newid(), 'Lorem', 'Ipsum', 'Lorus', 2, @restauracjaTajska, @rtCook),
-- Employees 'Restauracja staropolska'
(newid(), 'Jan', 'Piechota', 'Piechota', 3, @restauracjaStaropolska, @rsManager),
(newid(), 'Piotr', 'Nowak', 'Nowak', 1, @restauracjaStaropolska, @rsWaiter),
(newid(), 'Stanisław', 'Xiński', 'XXX', 2, @restauracjaStaropolska, @rsCook)

declare @managerWitek uniqueidentifier
select @managerWitek = RestaurantWorkerId from dbo.RestaurantWorker where Nick = 'Witek'
declare @managerJustys uniqueidentifier
select @managerJustys = RestaurantWorkerId from dbo.RestaurantWorker where Nick = 'Justyś'
declare @managerPeamo uniqueidentifier
select @managerPeamo = RestaurantWorkerId from dbo.RestaurantWorker where Nick = 'Peamo'
declare @managerPiechota uniqueidentifier
select @managerPiechota = RestaurantWorkerId from dbo.RestaurantWorker where Nick = 'Piechota'

-- Dodanie Managerów do restauracji
update dbo.Restaurant
set ManagerId = @managerWitek
where RestaurantId = @restauracjaWitka

update dbo.Restaurant
set ManagerId = @managerJustys
where RestaurantId = @restauracjaFrancuska

update dbo.Restaurant
set ManagerId = @managerPeamo
where RestaurantId = @restauracjaTajska

update dbo.Restaurant
set ManagerId = @managerPiechota
where RestaurantId = @restauracjaStaropolska

-- Dodanie Menu do restauracji
insert into dbo.Menu
(MenuId, Name, Code, RestaurantId)
values
(newid(), 'Tradycyjne Menu', 'TRAD', @restauracjaWitka),
(newid(), 'Kuchnia nowoczesna', 'NOWO', @restauracjaWitka),
(newid(), 'Francuskie Menu', 'FRANCJA', @restauracjaFrancuska),
(newid(), 'Cuda na kiju', 'CUDA', @restauracjaFrancuska),
(newid(), 'Tajskie Menu', 'TAJSKA', @restauracjaTajska),
(newid(), 'Piekelna kuchnia', 'PIEKŁO', @restauracjaTajska),
(newid(), 'Starodawne potrawy', 'STARE', @restauracjaStaropolska),
(newid(), 'Powiew świeżości', 'NOWE', @restauracjaStaropolska)

declare @restauracjaWitkaMenu1 uniqueidentifier
select @restauracjaWitkaMenu1 = MenuId from dbo.Menu where Code = 'TRAD'
declare @restauracjaWitkaMenu2 uniqueidentifier
select @restauracjaWitkaMenu2 = MenuId from dbo.Menu where Code = 'NOWO'
declare @restauracjaFrancuskaMenu1 uniqueidentifier
select @restauracjaFrancuskaMenu1 = MenuId from dbo.Menu where Code = 'FRANCJA'
declare @restauracjaFrancuskaMenu2 uniqueidentifier
select @restauracjaFrancuskaMenu2 = MenuId from dbo.Menu where Code = 'CUDA'
declare @restauracjaTajskaMenu1 uniqueidentifier
select @restauracjaTajskaMenu1 = MenuId from dbo.Menu where Code = 'TAJSKA'
declare @restauracjaTajskaMenu2 uniqueidentifier
select @restauracjaTajskaMenu2 = MenuId from dbo.Menu where Code = 'PIEKŁO'
declare @restauracjaStaropolskaMenu1 uniqueidentifier
select @restauracjaStaropolskaMenu1 = MenuId from dbo.Menu where Code = 'STARE'
declare @restauracjaStaropolskaMenu2 uniqueidentifier
select @restauracjaStaropolskaMenu2 = MenuId from dbo.Menu where Code = 'NOWE'

-- Dodanie kategorii produktów w restauracji
insert into dbo.ProductCategory
(ProductCategoryId, Name, Code, RestaurantId)
values
-- Restauracja u Witka
(newid(), 'Dania ciepłe', 'DC', @restauracjaWitka),
(newid(), 'Dania zimne', 'DZ', @restauracjaWitka),
(newid(), 'Napoje bezalkoholowe', 'NBA', @restauracjaWitka),
(newid(), 'Napoje alkoholowe', 'NA', @restauracjaWitka),
(newid(), 'Przekąski', 'PR', @restauracjaWitka),
-- Restauracja francuska
(newid(), 'Dania ciepłe', 'DC', @restauracjaFrancuska),
(newid(), 'Dania zimne', 'DZ', @restauracjaFrancuska),
-- Restauracja tajska
(newid(), 'Dania ciepłe', 'DC', @restauracjaTajska),
(newid(), 'Dania zimne', 'DZ', @restauracjaTajska),
-- Restauracja staropolska
(newid(), 'Dania ciepłe', 'DC', @restauracjaStaropolska),
(newid(), 'Dania zimne', 'DZ', @restauracjaStaropolska)

declare @restauracjaWitkaDaniaCieple uniqueidentifier
select @restauracjaWitkaDaniaCieple = ProductCategoryId from dbo.ProductCategory where Code = 'DC' AND RestaurantId = @restauracjaWitka
declare @restauracjaWitkaDaniaZimne uniqueidentifier
select @restauracjaWitkaDaniaZimne = ProductCategoryId from dbo.ProductCategory where Code = 'DZ' AND RestaurantId = @restauracjaWitka
declare @restauracjaWitkaNapojeBezalkoholowe uniqueidentifier
select @restauracjaWitkaNapojeBezalkoholowe = ProductCategoryId from dbo.ProductCategory where Code = 'NBA' AND RestaurantId = @restauracjaWitka
declare @restauracjaWitkaNapojeAlkoholowe uniqueidentifier
select @restauracjaWitkaNapojeAlkoholowe = ProductCategoryId from dbo.ProductCategory where Code = 'NA' AND RestaurantId = @restauracjaWitka
declare @restauracjaWitkaPrzekaski uniqueidentifier
select @restauracjaWitkaPrzekaski = ProductCategoryId from dbo.ProductCategory where Code = 'PR' AND RestaurantId = @restauracjaWitka
declare @restauracjaFrancuskaDaniaCieple uniqueidentifier
select @restauracjaFrancuskaDaniaCieple = ProductCategoryId from dbo.ProductCategory where Code = 'DC' AND RestaurantId = @restauracjaFrancuska
declare @restauracjaTajskaDaniaCieple uniqueidentifier
select @restauracjaTajskaDaniaCieple = ProductCategoryId from dbo.ProductCategory where Code = 'DC' AND RestaurantId = @restauracjaTajska
declare @restauracjaStaropolskaDaniaCieple uniqueidentifier
select @restauracjaStaropolskaDaniaCieple = ProductCategoryId from dbo.ProductCategory where Code = 'DC' AND RestaurantId = @restauracjaStaropolska

insert into dbo.Product
(ProductId, ProductCategoryId, MenuId, Name, [Description], Price, PhotoUrl)
values
-- Restauracja u Witka
(newid(), @restauracjaWitkaDaniaCieple, @restauracjaWitkaMenu1, 
	'Stek z kurczaka ze szpinakiem', 'Pierś pod szpinakiem, parmezan, frytki, surówki', 23, 'https://witek1902.github.io/restaurant-system/app-examples/stekzkurczaka.jpg'),
(newid(), @restauracjaWitkaDaniaCieple, @restauracjaWitkaMenu1, 
	'Świnia w pomidorach', 'Polędwiczki wieprzowe, pomidory, pieczarki, razowe knedliczki, kwaśna świetana, surówki', 26, 'https://witek1902.github.io/restaurant-system/app-examples/kotletwpomidorach.jpg'),
(newid(), @restauracjaWitkaDaniaZimne, @restauracjaWitkaMenu1, 
	'Tatar podany z marynatami', 'Mięso wołowe, marynaty, cebula, żółtko', 19, 'https://witek1902.github.io/restaurant-system/app-examples/tatar.jpg'),
(newid(), @restauracjaWitkaDaniaZimne, @restauracjaWitkaMenu1, 
	'Sałata z pieczoną piersią z kaczki', 'Sałaty, rucola, pierś z kaczki, gruszka, śliwki z octu, sos malinowy', 26, 'https://witek1902.github.io/restaurant-system/app-examples/pieczonapierszkaczki.jpg'),
(newid(), @restauracjaWitkaNapojeBezalkoholowe, @restauracjaWitkaMenu1, 
	'Sok pomarańczowy', 'CAPPY 200ml', 5, 'https://witek1902.github.io/restaurant-system/app-examples/cappy.jpg'),
(newid(), @restauracjaWitkaNapojeAlkoholowe, @restauracjaWitkaMenu1, 
	'Piwo Ciechan', 'miodowe', 8, 'https://witek1902.github.io/restaurant-system/app-examples/ciechan.jpg'),
(newid(), @restauracjaWitkaPrzekaski, @restauracjaWitkaMenu1, 
	'Gorące bułeczki prosto z pieca', 'Trzy rodzaje bułeczek, sosy', 8, 'https://witek1902.github.io/restaurant-system/app-examples/buleczki.jpg'),
(newid(), @restauracjaWitkaDaniaCieple, @restauracjaWitkaMenu2, 
	'Dorada w warzywach', 'Dorada, ziemniaczki pieczone, warzywa, masło', 27, 'https://witek1902.github.io/restaurant-system/app-examples/dorada.jpg'),
(newid(), @restauracjaWitkaDaniaCieple, @restauracjaWitkaMenu2, 
	'Stek Farmera (200gr)', 'Polędwica wołowa, fasola, cebula, natka pietruszki, bekon, pomidory, ziemniaki z ogniska, tzatziki', 39, 'https://witek1902.github.io/restaurant-system/app-examples/stekwolowy.jpg'),
(newid(), @restauracjaWitkaDaniaCieple, @restauracjaWitkaMenu2, 
	'Golonka pieczona – BAWARSKA', 'Golonka z pieca, sosy, gorące bułeczki, ogórek kiszony', 30, 'https://witek1902.github.io/restaurant-system/app-examples/golonka.jpg'),
-- Restauracja francuska
(newid(), @restauracjaFrancuskaDaniaCieple, @restauracjaFrancuskaMenu1, 
	'Piersi kaczki z sosem malinowym', 'Pierś kaczki, sól, piepatrz, czerwone wino, przyprawa piernikowa, świeże maliny, foie gras', 50, 'https://witek1902.github.io/restaurant-system/app-examples/francuska-piersi.jpg'),
(newid(), @restauracjaFrancuskaDaniaCieple, @restauracjaFrancuskaMenu1, 
	'Galettes bretonnes', 'Mąka gryczana niepalona, sól morska, olej kokosowy, szynka, ser, rolada kozia pleśniowa', 23, 'https://witek1902.github.io/restaurant-system/app-examples/francuska-galettes.jpg'),
(newid(), @restauracjaFrancuskaDaniaCieple, @restauracjaFrancuskaMenu1, 
	'Gulasz jagnięcy', 'Boczek, udzieć jagnięcy, pieczarki, czerwone wino wytrwane, marchewka, cebula', 50, 'https://witek1902.github.io/restaurant-system/app-examples/francuska-gulasz.jpg'),
(newid(), @restauracjaFrancuskaDaniaCieple, @restauracjaFrancuskaMenu2, 
	'Stek z sosem bearnaise i szparagami', 'Polędwica wołowa, świeżo mielony pieprz, zielone szparagi, białe wino, ocet winny', 83, 'https://witek1902.github.io/restaurant-system/app-examples/francuska-stek.jpg'),
(newid(), @restauracjaFrancuskaDaniaCieple, @restauracjaFrancuskaMenu2, 
	'Królik w czerwonym winie z suszonymi śliwkami', 'Tusza królika, goździki, imbir, cynamon, suszone śliwki, rodzynki, czerwone wino', 120, 'https://witek1902.github.io/restaurant-system/app-examples/francuska-krolik.jpg'),
(newid(), @restauracjaFrancuskaDaniaCieple, @restauracjaFrancuskaMenu2, 
	'Polędwiczki w sosie śliwkowym', 'Suszone śliwki, brandy, polędwiczki wieprzowe, śmietana, koperek, masło', 90, 'https://witek1902.github.io/restaurant-system/app-examples/francuska-poledwiczki.jpg'),
-- Restauracja tajska
(newid(), @restauracjaTajskaDaniaCieple, @restauracjaTajskaMenu1, 
	'Tajskie curry z ciecierzycą, dynią i szpinakiem', 'Ciecierzyca, dynia, cebula, czosnek, imbir, szpinak, limonka', 40, 'https://witek1902.github.io/restaurant-system/app-examples/tajska-curry.jpg'),
(newid(), @restauracjaTajskaDaniaCieple, @restauracjaTajskaMenu1, 
	'Krewetki w sosie curry z pomidorami koktajkowymi', 'Krewetki, czerwone curry, olej kokosowy, sos rybny, brązowy cukier', 67, 'https://witek1902.github.io/restaurant-system/app-examples/tajska-krewetki.jpg'),
(newid(), @restauracjaTajskaDaniaCieple, @restauracjaTajskaMenu1, 
	'Małże św. Jakuba po tajsku', 'Przegrzebki, świeżo starty imbir, czosnek, chili, kurkuma, olej kokosowy, kolendra', 100, 'https://witek1902.github.io/restaurant-system/app-examples/tajska-malze.jpg'),
(newid(), @restauracjaTajskaDaniaCieple, @restauracjaTajskaMenu2, 
	'Wegański Pad Thai', 'Makaron ryżowy, cebula, marchewka, tofu, sos rybny, kiełki fasoli mung, świeża kolendra', 33, 'https://witek1902.github.io/restaurant-system/app-examples/tajska-pad.jpg'),
(newid(), @restauracjaTajskaDaniaCieple, @restauracjaTajskaMenu2, 
	'Tajska zupa rybna curry', 'Dorsz, mleko kokosowe, imbir, czerwone curry, sos rybny, sos trzcinowy', 20, 'https://witek1902.github.io/restaurant-system/app-examples/tajska-zupa.jpg'),
(newid(), @restauracjaTajskaDaniaCieple, @restauracjaTajskaMenu2, 
	'Naleśniki kokosowe z papają i krmeme kokosowym', 'Mąka kasztanowa, krem z prawdziwego kokosa i świeża papaja', 30, 'https://witek1902.github.io/restaurant-system/app-examples/tajska-nalesniki.jpg'),
-- Restauracja staropolska
(newid(), @restauracjaStaropolskaDaniaCieple, @restauracjaStaropolskaMenu1, 
	'Polędwiczki w sosie musztardowym z pieczarkami', 'Polędwiczka wieprzowa, suszone oregano, masło, brandy, musztarda, śmietana', 23, 'https://witek1902.github.io/restaurant-system/app-examples/polska-poledwiczki.jpg'),
(newid(), @restauracjaStaropolskaDaniaCieple, @restauracjaStaropolskaMenu1, 
	'Kapuśniak z młodej kapusty z zieloną soczewicą i indykiem', 'Filet z indyka, soczewica, młoda kapusta, koncentrat pomidorowy, zioła prowansalskie', 33, 'https://witek1902.github.io/restaurant-system/app-examples/polska-kapusniak.jpg'),
(newid(), @restauracjaStaropolskaDaniaCieple, @restauracjaStaropolskaMenu1, 
	'Bitki wołowe', 'Mięso wołowe z udźca, przyprawa zbójnicka, czosnek, ziele angielskie, liść laurowy', 40, 'https://witek1902.github.io/restaurant-system/app-examples/polska-bitki.jpg'),
(newid(), @restauracjaStaropolskaDaniaCieple, @restauracjaStaropolskaMenu2, 
	'Polędwiczki wieprzowe z cydrem i karmelizowanymi jabłkami', 'Polędwiczka wieprzowa, szalotka, jabłka boiken, calvados', 50, 'https://witek1902.github.io/restaurant-system/app-examples/polska-jablka.jpg'),
(newid(), @restauracjaStaropolskaDaniaCieple, @restauracjaStaropolskaMenu2, 
	'Bigos pieczony z mielonym mięsem i grzybami', 'Łopatka, biała kapusta, kapusta kiszona, sos worcestershire, powidła śliwkowe, kwaśne jabłko', 23, 'https://witek1902.github.io/restaurant-system/app-examples/polska-bigos.jpg'),
(newid(), @restauracjaStaropolskaDaniaCieple, @restauracjaStaropolskaMenu2, 
	'Ligawa wołowa z sosem balsamicznym i ziemniakami', 'Ligawa wołowa, musztarda, ocet balsamiczny, miód, młode ziemniaki, sałata ze śmietaną', 100, 'https://witek1902.github.io/restaurant-system/app-examples/polska-ligawa.jpg')

declare @stekZKurczaka uniqueidentifier
select @stekZKurczaka = ProductId from Product where Name = 'Stek z kurczaka ze szpinakiem' and MenuId = @restauracjaWitkaMenu1
declare @swiniaWPomidorach uniqueidentifier
select @swiniaWPomidorach = ProductId from Product where Name = 'Świnia w pomidorach' and MenuId = @restauracjaWitkaMenu1
declare @sokPomaranczowy uniqueidentifier
select @sokPomaranczowy = ProductId from Product where Name = 'Sok pomarańczowy' and MenuId = @restauracjaWitkaMenu1

insert into dbo.ProductDetails
(ProductDetailsId, Protein, Carbohydrates, Fat, Calories)
values
(newid(), 100, 60, 40, 1000)

declare @stekZKurczakaDetails uniqueidentifier
select @stekZKurczakaDetails = ProductDetailsId from ProductDetails where Calories = 1000

update dbo.Product
set ProductDetailsId = @stekZKurczakaDetails
where ProductId = @stekZKurczaka

-- Pobranie konta klienta
declare @customer int
select @customer = UserId from dbo.AppUser where [Login] = 'customer'

-- Dodanie nowego klienta
insert into dbo.Customer
(CustomerId, Firstname, UserId)
values
(newid(), 'Janek', @customer)

declare @klientJanek uniqueidentifier
select @klientJanek = CustomerId from Customer where Firstname = 'Janek'

-- Dodanie zamówienia klienta Janka
insert into dbo.[Order]
(OrderId, CustomerId, CookId, WaiterId, OrderCreationDate, TableNumber, OrderStatusId, OrderFinishedDate, Comments)
values
(newid(), @klientJanek, null, null, GETDATE(), 12, 1, null, 'Proszę o przysienie trzech szklanek.')

declare @zamowienieJanka uniqueidentifier
select @zamowienieJanka = OrderId from dbo.[Order] where CustomerId = @klientJanek

-- Dodanie elementów zamówienia klienta Janka
insert into dbo.OrderItem
(OrderItemId, OrderItemStatusId, OrderId, Quantity, ProductId)
values
(newid(), 2, @zamowienieJanka, 1, @stekZKurczaka),
(newid(), 2, @zamowienieJanka, 1, @swiniaWPomidorach),
(newid(), 2, @zamowienieJanka, 2, @sokPomaranczowy)