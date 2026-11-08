class NodoSolicitud
{
    public int codigo;
    public string solicitante, tipoServicio, fecha, estado;
    public NodoSolicitud siguiente;

    public NodoSolicitud(int c, string s, string t, string f)
    {
        codigo = c;
        solicitante = s;
        tipoServicio = t;
        fecha = f;
        estado = "Pendiente";
        siguiente = null;
    }
}