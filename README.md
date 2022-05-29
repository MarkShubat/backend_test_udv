# backend_test_udv
В программе присутствуют три публичных API метода:
1) /VkPostsController/GetDictionary - этот метод принимает в качестве аргументов два числа - идентификатор пользователя ВКонтакте (id), а также количество последних постов на его странице (count), по которым будут сделаны вычисления (по умолчанию числа стоят так, чтобы вычисления проводились на основе 5 последних постов с моей страницы ВК). В результате работы метод вернет словарь, в котором ключами будут все различные буквы и цифры без учета регистра и знаков препинания в требуемом количестве последних постов на странице ВК, а значениями - количество этих букв и цифр.
2) /VkPostsController/GetDictionaryAndSave - метод работает также, как и предыдущий, но в дополнение к этому он сохраняет результат работы, а также дату и время обращения к этому методу. Данные сохраняются в бд PostgreSQL на локальном сервере. 
3) /VkPostsController/GetPostsHistory - метод возвращает все значения из базы данных т.е. историю составления словарей на основе работы второго публичного API метода.

Примечание: Методы 1,2 некорректно работают, если в постах будут содержаться символы в специфических кодировках!
