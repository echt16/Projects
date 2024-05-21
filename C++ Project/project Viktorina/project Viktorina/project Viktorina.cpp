#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <fstream>
#include <ostream>
#include <istream>
#include <cstdlib>
#include <string>
#include <random>
#include <ctime>

using namespace std;

string loginSplit(string userline)
{
    string log;
    for (int i = 0; i < size(userline); i++)
    {
        if (userline[i] == ':')
            break;
        log += userline[i];
    }
    return log;
}

string passwordSplit(string userline)
{
    string pas;
    for (int i = 0; i < size(userline); i++)
    {
        if (userline[i] == ':')
        {
            for (int j = i + 1; j < size(userline); j++)
            {
                pas += userline[j];
            }
            break;
        }
    }
    return pas;
}

void Restart()
{
    ifstream themes("Themes.txt");
    if (themes)
    {
        while (themes)
        {
            string theme;
            getline(themes, theme);
            string s = theme + '1' + ".txt";
            int a = s.length() + 1;
            char* str = new char[a];
            strcpy(str, s.c_str());
            remove(str);
            s = theme + '2' + ".txt";
            a = s.length() + 1;
            str = new char[a];
            strcpy(str, s.c_str());
            remove(str);
            s = theme + '3' + ".txt";
            a = s.length() + 1;
            str = new char[a];
            strcpy(str, s.c_str());
            remove(str);
        }
        string theme = "Themes";
        string s = theme + ".txt";
        int a = s.length() + 1;
        char* str = new char[a];
        strcpy(str, s.c_str());
        remove(str);

        ofstream t("Themes.txt");
        t << "Games\nCar\nSport\n";
        t.close();



        ofstream games1("Games1.txt");
        games1 << "#Which character is called \"Support\" ?\n*Restores health to self and allies\n*Does more damage than other classes\n*Has a large amount of health\n*Plays on the flank\n#What is called in the games \"Mana\" ?\n*Energy Points\n*Strong side of the character\n*Damage\n*Ability\n#What is called in the games \"Hp\" ?\n*Health Points\n*Critical Hit Chance\n*Speed\n*Main character ability\n#What is called in the games \"Ulta\" ?\n*Character ability\n*Chance to block an attack\n*Attack range\n*Attack speed\n#What does \"Skill-dependent character\" mean ?\n*Character to be able to play\n*A character that is easy to play\n*A character that is too strong\n*Character who is too weak\n";
        games1.close();

        ofstream games2("Games2.txt");
        games2 << "#What is the name of the part of GTA, in which the main character is CJ?\n*GTA San Andreas\n*GTA Vice City\n*GTA 4\n*GTA 5\n#What game is not available on mobile devices?\n*Battlefield Mobile\n*PUBG Mobile\n*League of legends Mobile\n*Call of duty Mobile\n#What is the highest rank in Brawl Stars?\n*35\n*50\n*25\n*40\n#What is the name of the game in which cars play football?\n*RocketLeague\n*Carball\n*Football at Wheels\n*Ride to Strike\n#Which block is impossible to destroy in Minecraft in survival mode?\n*Bedrock\n*Obsidian\n*Diamond block\n*Netherite block";
        games2.close();

        ofstream games3("Games3.txt");
        games3 << "#Which character in Warframe has the invisibility ability?\n*Loki\n*Rhino\n*Wukong\n*Nekros\n#Which character from the Mortal Kombat universe is Ed Boon's favorite?\n*Scorpion\n*Sub Zero\n*Liu Kang\n*Shao Kahn\n#What is the name of the main character from the game Horizon?\n*Eloy\n*Rast\n*Olin\n*Aid\n#Which character is present in Mortal Kombat Mobile?\n*Sub-Zero Cold War\n*Scorpion Ashbringer\n*Shao Kahn Destroyer\n*Kung Lao Classic\n#Which character is unobtainable in Warframe?\n*Excalibur Prime\n*Misa Prime\n*Nova Prime\n*Harrow Prime";
        games3.close();



        ofstream car1("Car1.txt");
        car1 << "#Who founded Ford?\n*Genry Ford\n*Margaret Ford\n*Angela Williams\n*Rosy Jordan\n#Which car manufacturer uses the slogan “Simply Clever”?\n*Skoda\n*BMW\n*Volkswagen\n*Lada\n#Which brand of car was founded by Lionel Martin and Robert Bamford?\n*Aston Martin\n*Lamborghini\n*Audi\n*Nissan\n#How is Vauxhall called in Europe?\n*Opel\n*Bentley\n*Ford\n*Ferrari\n#Which brand is a Beetle?\n*Volkswafen\n*Jaguar\n*Nissan\n*Lexus";
        car1.close();

        ofstream car2("Car2.txt");
        car2 << "#In which city was the first Grand Prix ?\n*United Kingdom\n*London\n*Manchester\n*Madrid\n#Who is the only driver to have completed the Triple Crown ?\n*Graham Hill\n*Michele Alboreto\n*Jean Alesi\n*Fernando Alonso\n#Which rally can only be joined by students with Renault 4 cars ?\n*4L Trophy\n*Acropolis Rally\n*M - Sport\n*Tanak\n#Which country is Honda from ?\n*Japanese\n*India\n*USA\n*Germany\n#In which American city can you find the headquarters of Chevrolet ?\n*Detroit\n*New York\n*Chicago\n*Houston";
        car2.close();

        ofstream car3("Car3.txt");
        car3 << "#In which UK city was Rolls Royce founded ?\n*Manchester\n*London\n*Newcastle\n*Milton\n#Which brand of car has the same name as a planet ?\n*Merkur Scorpio\n*BMW Venus\n*Mercedes Uranus\n*Tesla Earth\n#What is Japan’s largest - selling make of premium cars ?\n*Lexus\n*Honda\n*Toyota\n*BMW\n#Which car company made the Falcon ?\n*Ford\n*Chevrolet\n*Hyundai\n*Toyota\n#Which brand is a Grand Cherokee ?\n*Jeep\n*Peugeot\n*Suzuki\n*Land - Rover";
        car3.close();



        ofstream sport1("Sport1.txt");
        sport1 << "#What is not needed if there is power?\n*Mind\n*Experience\n*Problems\n*Agility\n#Sports equipment for pulling\n*Rope\n*Blanket\n*Thread\n*Other\n#Game with a melon ball\n*Rugby\n*Football\n*Volleyball\n*Other\n#Ancestors of sneakers\n*Keds\n*Boots\n*Sandals\n*Other\n#Sport referee tool\n*Whistle\n*Cards\n*Bat\n*Other";
        sport1.close();

        ofstream sport2("Sport2.txt");
        sport2 << "#What covered the gladiatorial arena in ancient Rome?\n*Sand\n*Rubble\n*left as is\n*Other\n#Who did not have the right to take part in the Olympic Games?\n*Women\n*Children\n*Negroes\n*All of this\n#How many sports are there in the world?\n*3000\n*5000\n*6000\n*4000\n#How often are the Olympic Games held?\n*once every four years\n*once every three years\n*once every five years\nother\n#What is the length of the marathon distance?\n*42195m\n*38585m\n*47122m\n*51900m";
        sport2.close();

        ofstream sport3("Sport3.txt");
        sport3 << "#In which game is the lightest ball used?\n*Handball\n*Tennis\n*Billiards\n*Other\n#How long is the running track in a sports stadium?\n*400m\n*500m\n*610m\n*1km\n#In what year did the 1st Winter Olympic Games take place?\n*1924\n*1981\n*1833\n*1814\n#Since 1924, one more word has been added to the words \"Olympic Games\". Which\n*Summer and winter\n*Early and late\n*Seasonal\n*They did not add words\n#A wreath of which leaves was awarded to the winners of the Olympic Games in Ancient Greece\n*Lavra\n*Mirra\n*Alder\n*Yew";
        sport3.close();



    }
    themes.close();

    ifstream users("Users.txt");
    if (users)
    {
        while (users)
        {
            string user;
            getline(users, user);
            user = loginSplit(user);
            string s = user + ".txt";
            int a = s.length() + 1;
            char* str = new char[a];
            strcpy(str, s.c_str());
            remove(str);
        }
    }
    users.close();

    remove("Users.txt");

    cout << "Сброс до заводских настроек успешно завершен";
}

void CorrectFile()
{
    cout << "Введите тему: ";
    string theme;
    getline(cin, theme);
    char sl;
    cout << "Введите уровень сложности (Легкий - 1; Средний - 2; Трудный - 3):";
    cin >> sl;
    cin.ignore();
    while (sl != '1' && sl != '2' && sl != '3')
    {
        cout << "Значение недопустимо, введите еще раз: ";
        cin >> sl;
        cin.ignore();
    }
    string b;
    ifstream f(theme + sl + ".txt", ios::in);
    while (!f.eof())
    {
        string line;
        getline(f, line);
        if (line == " " || line == "\n" || line.length() < 2)
        {
            continue;
        }
        else
        {
            b += line;
            b += "\n";
        }
    }
    f.close();
    ofstream of(theme + sl + ".txt", ios::out);
    cout << b;
    of << b;
    of.close();
}

void AppendQuestion()
{
    cout << "Введите тему: ";
    string theme;
    getline(cin, theme);
    char sl;
    cout << "Введите уровень сложности (Легкий - 1; Средний - 2; Трудный - 3):";
    cin >> sl;
    cin.ignore();
    while (sl != '1' && sl != '2' && sl != '3')
    {
        cout << "Значение недопустимо, введите еще раз: ";
        cin >> sl;
        cin.ignore();
    }
    string append = "#";
    cout << "Введите вопрос:";
    string quest;
    getline(cin, quest);
    append += quest;
    append += "\n*";
    cout << "Введите правильный вариант ответа: ";
    string v1;
    getline(cin, v1);
    append += v1;
    append += "\n*";
    cout << "Введите второй вариант ответа: ";
    getline(cin, v1);
    append += v1;
    append += "\n*";
    cout << "Введите третий вариант ответа: ";
    getline(cin, v1);
    append += v1;
    append += "\n*";
    cout << "Введите четвертый вариант ответа: ";
    getline(cin, v1);
    append += v1;
    append += "\n";
    ofstream outf(theme + sl + ".txt", ios::app);
    outf << append;
    outf.close();
    cout << "Вопрос добавлен." << endl;
    CorrectFile();
}

void AppendTheme()
{
    cout << "Введите название темы: ";
    string theme;
    getline(cin, theme);
    cout << "Вводите данные для легкого уровня: \n";
    cout << "Количество вопросов: ";
    int col;
    cin >> col;
    cin.ignore();
    for (int i = 0; i < col; i++)
    {
        ofstream outf(theme + "1" + ".txt", ios::ate);
        string append = "#";
        cout << "Введите вопрос:";
        string quest;
        getline(cin, quest);
        append += quest;
        append += "\n*";
        cout << "Введите правильный вариант ответа: ";
        string v1;
        getline(cin, v1);
        append += v1;
        append += "\n*";
        cout << "Введите второй вариант ответа: ";
        getline(cin, v1);
        append += v1;
        append += "\n*";
        cout << "Введите третий вариант ответа: ";
        getline(cin, v1);
        append += v1;
        append += "\n*";
        cout << "Введите четвертый вариант ответа: ";
        getline(cin, v1);
        append += v1;
        append += "\n";
        outf << append;
        outf.close();

        ofstream themes("Themes.txt", ios::app);
        themes << theme + "\n";
        themes.close();
    }
    cout << "Вводите данные для среднего уровня: \n";
    cout << "Количество вопросов: ";
    col;
    cin >> col;
    cin.ignore();
    for (int i = 0; i < col; i++)
    {
        ofstream outf(theme + "2" + ".txt", ios::ate);
        string append = "#";
        cout << "Введите вопрос:";
        string quest;
        getline(cin, quest);
        append += quest;
        append += "\n*";
        cout << "Введите правильный вариант ответа: ";
        string v1;
        getline(cin, v1);
        append += v1;
        append += "\n*";
        cout << "Введите второй вариант ответа: ";
        getline(cin, v1);
        append += v1;
        append += "\n*";
        cout << "Введите третий вариант ответа: ";
        getline(cin, v1);
        append += v1;
        append += "\n*";
        cout << "Введите четвертый вариант ответа: ";
        getline(cin, v1);
        append += v1;
        append += "\n";
        outf << append;
        outf.close();
    }
    cout << "Вводите данные для трудного уровня: \n";
    cout << "Количество вопросов: ";
    col;
    cin >> col;
    cin.ignore();
    for (int i = 0; i < col; i++)
    {
        ofstream outf(theme + "3" + ".txt", ios::ate);
        string append = "#";
        cout << "Введите вопрос:";
        string quest;
        getline(cin, quest);
        append += quest;
        append += "\n*";
        cout << "Введите правильный вариант ответа: ";
        string v1;
        getline(cin, v1);
        append += v1;
        append += "\n*";
        cout << "Введите второй вариант ответа: ";
        getline(cin, v1);
        append += v1;
        append += "\n*";
        cout << "Введите третий вариант ответа: ";
        getline(cin, v1);
        append += v1;
        append += "\n*";
        cout << "Введите четвертый вариант ответа: ";
        getline(cin, v1);
        append += v1;
        append += "\n";
        outf << append;
        outf.close();
    }
    cout << "Тема добавлена." << endl;
}

void DeleteTheme()
{
    cout << "Введите тему: ";
    string theme;
    getline(cin, theme);
    string s = theme + '1' + ".txt";
    int a = s.length() + 1;
    char* str = new char[a];
    strcpy(str, s.c_str());
    remove(str);
    s = theme + '2' + ".txt";
    a = s.length() + 1;
    str = new char[a];
    strcpy(str, s.c_str());
    remove(str);
    s = theme + '3' + ".txt";
    a = s.length() + 1;
    str = new char[a];
    strcpy(str, s.c_str());
    remove(str);
    cout << "Тема удалена." << endl;

    ifstream themes("Themes.txt");
    int count = 0;
    string h;
    while (themes)
    {
        getline(themes, h);
        if (themes)
            count++;
    }
    h = "";
    themes.close();
    string newthemes = "";

    ifstream themes1("Themes.txt");
    for (int i = 0; i < count; i++)
    {
        getline(themes1, h);
        if (h != theme)
            newthemes += h + "\n";
    }

    a = theme.length() + 1;
    char* str1 = new char[a];
    strcpy(str1, theme.c_str());
    remove(str1);
    themes1.close();

    ofstream themesnew("Themes.txt");
    themesnew << newthemes;
    themesnew.close();
}

void DeleteQuestion()
{
    cout << "Введите тему, вопрос в которой хотите удалить:";
    string theme;
    getline(cin, theme);
    char sl;
    cout << "Введите уровень сложности (Легкий - 1; Средний - 2; Трудный - 3):";
    cin >> sl;
    cin.ignore();
    while (sl != '1' && sl != '2' && sl != '3')
    {
        cout << "Значение недопустимо, введите еще раз: ";
        cin >> sl;
        cin.ignore();
    }
    string text;
    string q = "#";
    string q1;
    cout << "Введите вопрос, который нужно удалить: ";
    getline(cin, q1);
    q += q1;
    ifstream f(/*theme + sl + ".txt"*/ "abc1.txt", ios::in);
    if (!f)
    {
        cout << "Error!" << theme + sl + ".txt " << "не может быть открыт!" << endl;
        exit(1);
    }
    int g = 0;
    while (!f.eof())
    {
        string line;
        getline(f, line);
        if (line == q)
        {
            g = 5;
        }
        else if (g >= 9 || g < 5)
        {
            text += line;
            text += "\n";
        }
        else
        {
            g++;
        }
        //cout << line;
    }
    f.close();
    ofstream outf(/*theme + sl + ".txt"*/ "abc1.txt", ios::out);
    outf << text;
    outf.close();
    cout << "Вопрос удален." << endl;
}

void ChangeQuestion()
{
    cout << "Введите тему, вопрос в которой хотите изменить:";
    string theme;
    getline(cin, theme);
    char sl;
    cout << "Введите уровень сложности (Легкий - 1; Средний - 2; Трудный - 3):";
    cin >> sl;
    cin.ignore();
    while (sl != '1' && sl != '2' && sl != '3')
    {
        cout << "Значение недопустимо, введите еще раз: ";
        cin >> sl;
        cin.ignore();
    }
    string text;
    string q = "#";
    string q1;
    cout << "Введите вопрос, который нужно изменить: ";
    getline(cin, q1);
    q += q1;
    string n = "#";
    string n1;
    cout << "Введите вопрос, на который нужно изменить: ";
    getline(cin, n1);
    n += n1;
    ifstream f(/*theme + sl + ".txt"*/ "abc1.txt", ios::in);
    if (!f)
    {
        cout << "Error!" << theme + sl + ".txt " << "не может быть открыт!" << endl;
        exit(1);
    }
    int g = 0;
    while (!f.eof())
    {
        string line;
        getline(f, line);
        if (line == q)
        {
            text += n;
            text += "\n";
            g = 5;
        }
        else if (g < 5 || g >= 9)
        {
            text += line;
            text += "\n";
        }
        else if (g == 5)
        {
            cout << "Введите правильный вариант ответа: ";
            string j = "*";
            string j1;
            getline(cin, j1);
            j += j1;
            text += j;
            text += "\n";
            g++;
        }
        else
        {
            cout << "Введите вариант ответа: ";
            string j = "*";
            string j1;
            getline(cin, j1);
            j += j1;
            text += j;
            text += "\n";
            g++;
        }
        //cout << line;
    }
    f.close();
    ofstream outf(/*theme + sl + ".txt"*/ "abc1.txt", ios::out);
    outf << text;
    outf.close();
    cout << "Вопрос изменен." << endl;
    CorrectFile();
}

void ChangeVariant()
{
    cout << "Введите тему, вариант ответа в которой хотите изменить:";
    string theme;
    getline(cin, theme);
    char sl;
    cout << "Введите уровень сложности (Легкий - 1; Средний - 2; Трудный - 3):";
    cin >> sl;
    cin.ignore();
    while (sl != '1' && sl != '2' && sl != '3')
    {
        cout << "Значение недопустимо, введите еще раз: ";
        cin >> sl;
        cin.ignore();
    }
    string text;
    string q = "#";
    string q1;
    cout << "Введите вопрос, в котором нужно изменить вариант ответа: ";
    getline(cin, q1);
    q += q1;
    string n = "*";
    string n1;
    cout << "Введите вариант ответа, который нужно изменить: ";
    getline(cin, n1);
    n += n1;
    string v = "*";
    string v1;
    cout << "Введите вариант ответа, на который нужно изменить: ";
    getline(cin, v1);
    v += v1;
    ifstream f(/*theme + sl + ".txt"*/ "abc1.txt", ios::in);
    if (!f)
    {
        cout << "Error!" << theme + sl + ".txt " << "не может быть открыт!" << endl;
        exit(1);
    }
    int g1 = 0;
    int g = 0;
    while (!f.eof())
    {
        string line;
        getline(f, line);
        if (line == q)
        {
            text += line;
            text += "\n";
            g = 5;
        }
        else if (g >= 9 || g < 5)
        {
            text += line;
            text += "\n";
        }
        else
        {
            if (line == n && g1 != 0)
            {
                text += v;
                text += "\n";
            }
            else
            {
                text += line;
                text += "\n";
                g1++;
            }
            g++;
        }
    }
    cout << text;
    f.close();
    ofstream outf(/*theme + sl + ".txt"*/ "abc1.txt", ios::out);
    outf << text;
    outf.close();
    cout << "Вариант ответа изменен." << endl;
    CorrectFile();
}

void ChangeRightVariant()
{
    cout << "Введите тему, вариант ответа в которой хотите изменить:";
    string theme;
    getline(cin, theme);
    char sl;
    cout << "Введите уровень сложности (Легкий - 1; Средний - 2; Трудный - 3):";
    cin >> sl;
    cin.ignore();
    while (sl != '1' && sl != '2' && sl != '3')
    {
        cout << "Значение недопустимо, введите еще раз: ";
        cin >> sl;
        cin.ignore();
    }
    string text;
    string q = "#";
    string q1;
    cout << "Введите вопрос, в котором нужно изменить вариант ответа: ";
    getline(cin, q1);
    q += q1;
    string n = "*";
    string n1;
    cout << "Введите правильный вариант: ";
    getline(cin, n1);
    n += n1;
    ifstream f(/*theme + sl + ".txt"*/ "abc1.txt", ios::in);
    if (!f)
    {
        cout << "Error!" << theme + sl + ".txt " << "не может быть открыт!" << endl;
        exit(1);
    }
    int g1 = 0;
    int g = 0;
    while (!f.eof())
    {
        string line;
        getline(f, line);
        if (line == q)
        {
            text += line;
            text += "\n";
            g = 5;
        }
        else if (g == 5)
        {
            text += n;
            text += "\n";
            g++;
        }
        else
        {
            text += line;
            text += "\n";
        }
        //cout << line;
    }
    f.close();
    ofstream outf(/*theme + sl + ".txt"*/ "abc1.txt", ios::out);
    outf << text;
    outf.close();
    cout << "Правильный вариант ответа изменен." << endl;
    CorrectFile();
}

void ChangeLevel()
{
    cout << "Введите тему, уровень сложности которой нужно изменить: ";
    string theme;
    getline(cin, theme);
    char sl;
    cout << "Введите уровень сложности (Легкий - 1; Средний - 2; Трудный - 3):";
    cin >> sl;
    cin.ignore();
    while (sl != '1' && sl != '2' && sl != '3')
    {
        cout << "Значение недопустимо, введите еще раз: ";
        cin >> sl;
        cin.ignore();
    }
    cout << "Количество вопросов: ";
    int col;
    cin >> col;
    cin.ignore();
    for (int i = 0; i < col; i++)
    {
        ofstream outf(theme + "1" + ".txt", ios::ate);
        string append = "#";
        cout << "Введите вопрос:";
        string quest;
        getline(cin, quest);
        append += quest;
        append += "\n*";
        cout << "Введите правильный вариант ответа: ";
        string v1;
        getline(cin, v1);
        append += v1;
        append += "\n*";
        cout << "Введите второй вариант ответа: ";
        getline(cin, v1);
        append += v1;
        append += "\n*";
        cout << "Введите третий вариант ответа: ";
        getline(cin, v1);
        append += v1;
        append += "\n*";
        cout << "Введите четвертый вариант ответа: ";
        getline(cin, v1);
        append += v1;
        append += "\n";
        outf << append;
        outf.close();
    }
    cout << "Уровень сложности изменен." << endl;
    CorrectFile();
}

void Show()
{
    cout << "Введите тему, вопросы которой нужно показать: ";
    string theme;
    getline(cin, theme);
    char sl;
    cout << "Введите уровень сложности (Легкий - 1; Средний - 2; Трудный - 3):";
    cin >> sl;
    cin.ignore();
    while (sl != '1' && sl != '2' && sl != '3')
    {
        cout << "Значение недопустимо, введите еще раз: ";
        cin >> sl;
        cin.ignore();
    }
    ifstream inf(theme + sl + ".txt");
    if (!inf)
    {
        cout << "Error! Test.txt could not be opened for reading!" << endl;
        exit(1);
    }
    int g = 0;
    while (!inf.eof())
    {
        string line;
        getline(inf, line);
        if (line[0] == '#')
        {
            cout << "Вопрос: ";
            for (int i = 1; i < size(line); i++)
            {
                cout << line[i];
            }
            cout << endl;
            g = 5;
        }
        else
        {
            if (g == 5)
            {
                cout << "Правильный вариант ответа: ";
                for (int i = 1; i < size(line); i++)
                {
                    cout << line[i];
                }
                cout << endl;
                g = 1;
            }
            else
            {
                cout << "Вариант ответа номер " << g << ": ";
                for (int i = 1; i < size(line); i++)
                {
                    cout << line[i];
                }
                cout << endl;
                g++;
            }
        }
    }
    inf.close();
}



void Menu()
{
    cout << "Здавствуйте, админ!" << endl;
    cout << "Выберите задачу:\nДобавить вопрос к определенной теме - 1\nДобавить тему - 2\nУдалить тему - 3\nУдалить вопрос в определенной теме - 4\nПоменять вопрос в определенной теме - 5\nИзменить вариант ответа в определенном вопросе - 6\nИзменить правильный вариант ответа в определенном вопросе - 7\nИзменить уровень сожности определенной темы - 8\nПоказать список определенных вопросов - 9\nСкорректировать файл - 10\nСброс до заводских настроек - 11\n" << endl;
    int g;
    cin >> g;
    cin.ignore();
    while (!(g > 0) && !(g <= 10))
    {
        cout << "Вы ввели недопустимое число. Введите вариант еще раз: ";
        cin >> g;
        cin.ignore();
    }
    switch (g)
    {
    case 1:
    {
        AppendQuestion();
    }break;
    case 2:
    {
        AppendTheme();
    }break;
    case 3:
    {

        DeleteTheme();
    }break;
    case 4:
    {
        DeleteQuestion();
    }break;
    case 5:
    {
        ChangeQuestion();
    }break;
    case 6:
    {
        ChangeVariant();
    }break;
    case 7:
    {
        ChangeRightVariant();
    }break;
    case 8:
    {
        ChangeLevel();
    }break;
    case 9:
    {
        Show();
    }break;
    case 10:
    {
        CorrectFile();
    }break;
    case 11:
    {
        Restart();
    }break;
    }
}











bool checkLogin(string login)
{
    ifstream users("Users.txt", ios::app);
    bool exist = false;
    string userline;
    string log;
    while (users)
    {
        getline(users, userline);
        log = loginSplit(userline);

        if (log == login)
        {
            exist = true;
            break;
        }
    }
    return exist;
    users.close();
}

bool checkPassword(string login, string password)
{
    ifstream users("Users.txt", ios::app);
    bool pastrue = false;
    string userline;
    string log;
    string pas;
    while (users)
    {
        getline(users, userline);
        log = loginSplit(userline);

        if (log == login)
        {
            pas = passwordSplit(userline);
            if (pas == password)
            {
                //cout << "Пароль верный!\n";
                pastrue = true;
            }
            else
                //cout << "Пароль неверный!\n";
                break;
        }
    }
    return pastrue;
    users.close();
}


void registration()
{
    cin.ignore();
    ifstream users("Users.txt", ios::app);

    cout << "Напишите Ваш новый логин: ";
    bool notExist = true;
    string newlogin;
    getline(cin, newlogin);
    notExist = !checkLogin(newlogin);

    ofstream users1("Users.txt", ios::app);

    if (notExist)
    {
        cout << "Напишите пароль: ";
        string newpassword;
        getline(cin, newpassword);
        users1 << newlogin << ":" << newpassword << "\n";

        ifstream user(newlogin + ".txt", ios::app);
    }

    else
        cout << "Такой логин уже существует!\n";

    users.close();
    users1.close();
}

bool enter(string login, string password)
{
    bool x = false;
    if (checkLogin(login))
    {
        if (checkPassword(login, password))
        {
            cout << "Логин и пароль верные!\n";
            x = true;
        }
        else
        {
            cout << "Неверный пароль!\n";
        }
    }
    else
    {
        cout << "Такого логина не существует!\n";
    }

    return x;
}

void addResult(string login, string theme, int level, int score)
{
    ofstream result(login + ".txt", ios::app);
    result << theme << ":" << level << ":" << score << "\n";
    result.close();
}

void checkResult(string login)
{
    ifstream result(login + ".txt", ios::app);
    string resultline;
    cout << "Тема:Сложность:Результат" << endl;
    while (result)
    {
        getline(result, resultline);
        cout << resultline << endl;
    }
    result.close();
}

void game(string login)
{
    cout << "Выберите тему:\n";
    ifstream themes("Themes.txt");
    string s;
    while (themes)
    {
        getline(themes, s);
        if (themes)
        {
            cout << s << endl;
        }
    }
    themes.close();
    getline(cin, s);
    cout << "Выберите сложность(1, 2, 3): ";
    int x;
    string x1;
    cin >> x;
    switch (x)
    {
    case 1:
        x1 = "1";
        break;
    case 2:
        x1 = "2";
        break;
    case 3:
        x1 = "3";
        break;
    }
    ifstream theme(s + x1 + ".txt");
    if (!theme)
    {
        cout << "Error! Такой темы или сложности нет!" << endl;
    }
    else
    {
        string q;
        string a, a1, a2, a3;
        char m;
        int score = 0;
        cout << "Игра началась!";
        while (theme)
        {
            theme >> m;
            getline(theme, q);
            theme >> m;
            getline(theme, a);
            theme >> m;
            getline(theme, a1);
            theme >> m;
            getline(theme, a2);
            theme >> m;
            getline(theme, a3);

            srand(time(NULL));
            int r = rand() % 4 + 1;
            if (theme)
            {
                cout << endl << q << endl << endl;
                switch (r)
                {
                case 1:
                    cout << "1. " << a << endl << "2. " << a1 << endl << "3. " << a2 << endl << "4. " << a3 << endl;
                    break;
                case 2:
                    cout << "1. " << a1 << endl << "2. " << a << endl << "3. " << a2 << endl << "4. " << a3 << endl;
                    break;
                case 3:
                    cout << "1. " << a1 << endl << "2. " << a2 << endl << "3. " << a << endl << "4. " << a3 << endl;
                    break;
                case 4:
                    cout << "1. " << a1 << endl << "2. " << a2 << endl << "3. " << a3 << endl << "4. " << a << endl;
                    break;
                }
                cout << endl;
                cout << "Выберите ответ (1, 2, 3, 4): ";
                int w;
                cin >> w;
                if (w == r)
                    score++;
                cout << endl;
            }
        }
        cout << score << " правильных ответов!" << endl << endl;
        addResult(login, s, x, score);
    }
}

void gameMenu()
{
    bool play = true;
    while (play)
    {
        int choose = 33;
        while (choose < 0 || choose > 3)
        {
            cout << "Выберите действие:\n1. Вход\n2. Регистрация\n3. Посмотреть результаты\n0. Выйти\n";
            cin >> choose;
            if (choose < 0 || choose > 3)
            {
                cout << "Такого варианта нет!";
            }
        }
        switch (choose)
        {
        case 0:
        {
            cout << "Вы вышли из игры!";
            play = false;
            break;
        }
        case 1:
        {
            string login, password;
            cout << "Введите логин: ";
            cin.ignore();
            getline(cin, login);

            if (login == "Admin12345")
            {
                cout << "Здраствуйте Админ!\n";
                Menu();
            }
            else
            {
                cout << "Введите пароль: ";
                getline(cin, password);

                if (enter(login, password))
                {
                    game(login);
                }
            }
            break;
        }
        case 2:
        {
            registration();
            break;
        }
        case 3:
        {
            string login;
            cout << "Введите логин, чтобы посмотреть результаты: ";
            cin.ignore();
            getline(cin, login);
            if (checkLogin(login))
            {
                checkResult(login);
            }
            else
            {
                cout << "Пользователь не найден!\n";
            }
            break;
        }
        }
    }
}

int main()
{
    setlocale(LC_ALL, "Russian");
    gameMenu();
    //Admin12345 - логин админа
}
