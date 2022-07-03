using UnityEngine;
using Random = UnityEngine.Random;

public class Platform : MonoBehaviour
{
public GameObject [] platforms; //платформы
public GameObject prefabStart; //префаб начала
public GameObject prefabMove; // префаб углов
public GameObject prefabFinish;// префаб конца
public static GameObject Startingposition;
private static Vector3 _startPos;
public static int [,,] _terrain; //матрица ландшафта
private static int _start; //стартовая высота
private int _lng = 3; //позиция в длину
private int _posH;
public static int Width;
public static int Height;
public static int Decoration;

private void Start() 
{
    _start = Random.Range(4,Height-3);
    _terrain = new int [Height,Width,2];
    _terrain[_start,2,0] = 1;
    _startPos = new Vector3(2f,0.3f,_start);
    Startingposition = Instantiate(prefabStart, _startPos, Quaternion.identity);
    _posH = _start;
    while(_lng<Width-3)
    {
        var leftObj = _terrain[_posH,_lng-1,0] == 0 || _terrain[_posH,_lng-1,0] == 2 || _terrain[_posH,_lng-1,0] == 6 || _terrain[_posH,_lng-1,0] == 7;
        if(_posH == Height-4) //случай при верхнем крае
        {
                if(_terrain[_posH-1,_lng,0] == 0)stick90();
                else cornerP(); 
                if(Random.Range(0,10)<5)stick90();
                else cornerC();
        }
        else if(_posH == 4) // случай при нижнем крае
        {
                if(_terrain[_posH+1,_lng,0] == 0)stick90();
                else cornerL();
                if(Random.Range(0,10)<5)stick90();
                else cornerJ();
        }
        else if(Random.Range(0,10)<5) //случайный выбор угла или линии 
        {
            if(leftObj)stick();
            else stick90();
        }
        else if(leftObj) //выбор угла
        {
            if(_terrain[_posH+1,_lng,0] == 0)cornerP();
            else cornerL();
        }
        else
        {
            if(Random.Range(0,10)<5)cornerJ();
            else cornerC();
        }
    }


    if(_terrain[_posH,_lng-1,0] == 3 || _terrain[_posH,_lng-1,0] == 4 || _terrain[_posH,_lng-1,0] == 5)//генерация конца пути
    {
        _terrain[_posH,_lng,0] = 8;
        Instantiate(prefabFinish, new Vector3(_lng,0.3f,_posH), Quaternion.identity);
    }
    else if(_terrain[_posH-1,_lng,0] == 6)
    {
        _terrain[_posH,_lng,0] = 4;
        Instantiate(prefabMove, new Vector3(_lng,0.3f,_posH), Quaternion.identity);
        _terrain[_posH,_lng+1,0] = 8;
        Instantiate(prefabFinish, new Vector3(_lng+1,0.3f,_posH), Quaternion.identity);
    }
    else
    {
        _terrain[_posH,_lng,0] = 5;
        Instantiate(prefabMove, new Vector3(_lng,0.3f,_posH), Quaternion.identity);
        _terrain[_posH,_lng+1,0] = 8;
        Instantiate(prefabFinish, new Vector3(_lng+1,0.3f,_posH), Quaternion.identity);
    }


    for (var v = 1; v < Width -1; v++) //заполнение пустых обьектов
    {
        for(var n = 1;n < Height -1;n++)
        {
            _terrain[n,v,1] = 11;
        }
    }
    decor();
    load();
}

private static void decor()
{
    for (var a = 1; a < Width -1; a++) 
    {
        for(var b = 1;b < Height -1;b++)
        {
            if (_terrain[b, a, 0] != 0 || Random.Range(0, 10) >= Decoration) continue;
            if(Random.Range(0,10)<5)
            {
                _terrain[b,a,1] = 9;
            }
            else
            {
                _terrain[b,a,1] = 10;
            }
        }
    }
}

private void load()
{
    for (var y = 0; y < Height; y++)
    {
        for (var x = 0; x < Width; x++) 
        {
            for(var z = 0;z < 2;z++) 
            {
                    Instantiate(platforms[_terrain[y,x,z]], new Vector3(x,z*0.3f,y), Quaternion.identity);
            }
        }
    }
}

private void stick()
{
    if(_terrain[_posH+1,_lng,0] == 0)
    {
        _terrain[_posH,_lng,0] = 2;
        _posH ++;
    }
    else
    {
        _terrain[_posH,_lng,0] = 2;
        _posH --;
    }
}

private void stick90()
{
    _terrain[_posH,_lng,0] = 3;
    _lng++;
}

private void cornerP()
{
    _terrain[_posH,_lng,0] = 4;
    Instantiate(prefabMove, new Vector3(_lng,0.3f,_posH), Quaternion.identity);
    _lng++;
}

private void cornerL()
{
    _terrain[_posH,_lng,0] = 5;
    Instantiate(prefabMove, new Vector3(_lng,0.3f,_posH), Quaternion.identity);
    _lng++;
}

private void cornerJ()
{
    _terrain[_posH,_lng,0] = 6;
    Instantiate(prefabMove, new Vector3(_lng,0.3f,_posH), Quaternion.identity);
    _posH++;
}

private void cornerC()
{
    _terrain[_posH,_lng,0] = 7;
    Instantiate(prefabMove, new Vector3(_lng,0.3f,_posH), Quaternion.identity);
    _posH--;
}
}