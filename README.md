# VeloRent
Test assignment: ASP.NET MVC 5 Web Application

	Допущения при построении БД
- Велосипед может быть взят в одной точке и сдан в другой;
- Названия велосипедов не могут совпадать
- Дата реальной сдачи велосипеда может отличаться от предполагаемой.
- Заявки оформляются по порядку (последующая дата не может быть меньше предыдущей). 

	Проверки при добавлении новых записей (реализованы в виде хранимых процедур)
- Если велосипед уже взят (поле Taken=1), то при создании новой заявки выдаем ошибку;
- При добавлении заявки увеличивается к-во раз, когда велосипед был взят; При удалении – уменьшается; При обновлении - (если значение было введено случайно, неверно), то у первого велосипеда делаем количество взятых раз -1 (timest -1), а у второго +1.
- Если при создании новой заявки велосипед находится в другой точке выдачи (определяем по дате), то выдаем ошибку.