# TestWebApp1
<div class="about">
    <p>Тестовое web-приложение, реализующее следующие функции:</p>
    <ul>Обязательные функции:
        <li>просмотр структуры предприятия (просмотр в виде дерева)</li>
        <li>просмотр списка сотрудников выбранного отдела </li>
        <li>просмотр и редактирование данных выбранного сотрудника. Помимо хранимой в БД информации необходимо отображать возраст сотрудника на текущий момент.
        <li>добавление сотрудников</li>
        <li>удаление сотрудника</li>
    </ul>
    <ul>Дополнительные функции:
        <li>просмотр и редактирование данных выбранного отдела</li>
        <li>добавление отдела</li>
        <li>удаление отдела</li>
    </ul>
</div>
<h1 class="remark">Примечания:</h1>
<div class="about">
    <ul>Тестовая база данных предоставлена с условиями задачи, имеются некоторые особенности:
        <li>модель данных из тестовой БД менять нельзя, поэтому soft delete не реализован.</li>
        <li>в тестовой базе имеется опечатка в наименовании таблицы ("Empoyee" вместо "Emp<b>l</b>oyee") - это отражено в "ApplicationContext.cs" в привязке класса к таблице. </li>
        <li>для Front-end требуется не использовать сторонние фреймворки</li>
    </ul>
    <ul>Дополнительная информация:
        <li>возраст сотрудников считается с округлением до года.</li>
        <li>настройки подключения к базе данных находятся в файле appsettings.json</li>
        <li>для работы с БД в проекте используется ORM "Entity Framework Core"</li>
        <li>добавлена проверка корректности ввода данных на стороне приложения</li>
        <li>отдел нельзя назначить родительским самому себе</li>
        <li>нельзя удалить отдел с подотделами или сотрудниками в отделе</li>
        <li>реализовано подтверждение удаления отдела и сотрудника</li>
        <li>дизайн выполнен в минималистичном стиле</li>
    </ul>
</div>
