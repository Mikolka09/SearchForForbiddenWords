# SearchForForbiddenWords(C#, WinAPI, Threading)
Реализовать приложение, позволяющее искать некоторый набор запрещенных слов в файлах.
Пользовательский интерфейс приложения должен позволять ввести или загрузить из файла набор запрещенных слов. При нажатии на кнопку «Старт», приложение должно начать 
искать эти слова на всех доступных накопителях информации (жесткие диски, флешки).
Файлы, содержащие запрещенные слова, должны быть скопированы в заданную папку.
Кроме оригинального файла, нужно создать новый файл с содержимым оригинального файла, в котором запрещенные слова заменены на 7 повторяющихся звезд (*******).
Также нужно создать файл отчета. Он должен содержать информацию о всех найденных файлах с запрещенными словами, пути к этим файлам, размер файлов, информацию о 
количестве замен и так далее. В файле отчета нужно также отобразить топ-10 самых популярных запрещенных слов.
Интерфейс программы должен показывать прогресс работы приложения с помощью индикаторов (progress bars). Пользователь через интерфейс приложения может приостановить 
работу алгоритма, возобновить, полностью остановить.
По итогам работы программы необходимо вывести результаты работы в элементы пользовательского интерфейса (нужно продумать, какие элементы управления понадобятся).
Программа обязательно должна использовать механизмы многопоточности и синхронизации!
Программа может быть запущена только в одной копии. Предусмотреть возможность запуска приложения из командной строки без отображения визуального интерфейса.

Implement an application that allows you to search for a certain set of forbidden words in files.
The user interface of the application must allow a set of forbidden words to be entered or loaded from a file. When you click on the "Start" button, the application should start
search for these words on all available storage devices (hard drives, flash drives).
Files containing forbidden words must be copied to the specified folder.
In addition to the original file, you need to create a new file with the contents of the original file, in which the forbidden words are replaced with 7 repeating stars (*******).
You also need to create a report file. It should contain information about all found files with forbidden words, paths to these files, file size, information about
the number of replacements and so on. The report file should also display the top 10 most popular forbidden words.
The program interface should show the progress of the application using progress bars. The user through the application interface can pause
algorithm work, resume, stop completely.
Based on the results of the program's work, it is necessary to display the results of the work in the elements of the user interface (you need to think over what controls will be needed).
The program must use the mechanisms of multithreading and synchronization!
The program can only be run in one copy. Provide the ability to launch the application from the command line without displaying the visual interface.
