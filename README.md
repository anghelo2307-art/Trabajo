using System;

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

class GestorBiblioteca
{
    NodoSolicitud cabeza = null;

    int LeerCodigo()
    {
        int codigo;
        while (!int.TryParse(Console.ReadLine(), out codigo) || codigo <= 0)
            Console.Write("Codigo invalido. Ingrese uno positivo: ");
        return codigo;
    }

    void Pausar()
    {
        Console.WriteLine("\nPresione Enter para continuar...");
        Console.ReadLine();
    }

    NodoSolicitud Buscar(int codigo)
    {
        NodoSolicitud actual = cabeza;
        while (actual != null)
        {
            if (actual.codigo == codigo)
                return actual;
            actual = actual.siguiente;
        }
        return null;
    }

    void Mostrar(NodoSolicitud s)
    {
        Console.WriteLine("Codigo      : " + s.codigo);
        Console.WriteLine("Solicitante : " + s.solicitante);
        Console.WriteLine("Servicio    : " + s.tipoServicio);
        Console.WriteLine("Fecha       : " + s.fecha);
        Console.WriteLine("Estado      : " + s.estado);
    }

    public void Registrar()
    {
        Console.Write("Ingrese codigo: ");
        int codigo = LeerCodigo();

        if (Buscar(codigo) != null)
        {
            Console.WriteLine("El codigo ya existe.");
            Pausar();
            return;
        }

        Console.Write("Nombre del solicitante: ");
        string nombre = Console.ReadLine();

        Console.Write("Tipo de servicio: ");
        string servicio = Console.ReadLine();

        Console.Write("Fecha (DD/MM/AAAA): ");
        string fecha = Console.ReadLine();

        NodoSolicitud nuevo = new NodoSolicitud(
            codigo, nombre, servicio, fecha);

        if (cabeza == null)
            cabeza = nuevo;
        else
        {
            NodoSolicitud actual = cabeza;
            while (actual.siguiente != null)
                actual = actual.siguiente;
            actual.siguiente = nuevo;
        }

        Console.WriteLine("Solicitud registrada correctamente.");
        Pausar();
    }

    public void BuscarSolicitud()
    {
        Console.Write("Ingrese codigo: ");
        int codigo = LeerCodigo();
        NodoSolicitud s = Buscar(codigo);

        if (s != null)
            Mostrar(s);
        else
            Console.WriteLine("No se encontro la solicitud.");

        Pausar();
    }

    public void Modificar()
    {
        Console.Write("Ingrese codigo: ");
        int codigo = LeerCodigo();
        NodoSolicitud s = Buscar(codigo);

        if (s == null)
        {
            Console.WriteLine("No se encontro la solicitud.");
            Pausar();
            return;
        }

        Console.Write("Nuevo tipo de servicio: ");
        s.tipoServicio = Console.ReadLine();

        Console.Write("Nuevo estado (Pendiente/Atendido/Devuelto): ");
        s.estado = Console.ReadLine();

        Console.WriteLine("Solicitud modificada correctamente.");
        Pausar();
    }

    public void Eliminar()
    {
        Console.Write("Ingrese codigo: ");
        int codigo = LeerCodigo();

        if (cabeza == null)
        {
            Console.WriteLine("La lista esta vacia.");
            Pausar();
            return;
        }

        if (cabeza.codigo == codigo)
        {
            cabeza = cabeza.siguiente;
            Console.WriteLine("Solicitud eliminada correctamente.");
            Pausar();
            return;
        }

        NodoSolicitud actual = cabeza;

        while (actual.siguiente != null &&
               actual.siguiente.codigo != codigo)
            actual = actual.siguiente;

        if (actual.siguiente != null)
        {
            actual.siguiente = actual.siguiente.siguiente;
            Console.WriteLine("Solicitud eliminada correctamente.");
        }
        else
            Console.WriteLine("No se encontro la solicitud.");

        Pausar();
    }

    public void MostrarSolicitudes()
    {
        NodoSolicitud actual = cabeza;

        if (actual == null)
            Console.WriteLine("No hay solicitudes registradas.");

        while (actual != null)
        {
            Console.WriteLine("\n--------------------");
            Mostrar(actual);
            actual = actual.siguiente;
        }

        Pausar();
    }

    public void BuscarNombre()
    {
        Console.Write("Ingrese nombre del solicitante: ");
        string nombre = Console.ReadLine();
        NodoSolicitud actual = cabeza;
        bool encontrado = false;

        while (actual != null)
        {
            if (actual.solicitante.Equals(
                nombre, StringComparison.OrdinalIgnoreCase))
            {
                Mostrar(actual);
                encontrado = true;
            }
            actual = actual.siguiente;
        }

        if (!encontrado)
            Console.WriteLine("No se encontro ese solicitante.");

        Pausar();
    }

    public void MostrarPendientes()
    {
        NodoSolicitud actual = cabeza;
        bool encontrado = false;

        while (actual != null)
        {
            if (actual.estado.Equals(
                "Pendiente", StringComparison.OrdinalIgnoreCase))
            {
                Mostrar(actual);
                Console.WriteLine("--------------------");
                encontrado = true;
            }
            actual = actual.siguiente;
        }

        if (!encontrado)
            Console.WriteLine("No hay solicitudes pendientes.");

        Pausar();
    }

    public void Estadisticas()
    {
        int total = 0, pendientes = 0;
        int atendidas = 0, devueltas = 0;
        NodoSolicitud actual = cabeza;

        while (actual != null)
        {
            total++;

            if (actual.estado.Equals("Pendiente",
                StringComparison.OrdinalIgnoreCase))
                pendientes++;
            else if (actual.estado.Equals("Atendido",
                StringComparison.OrdinalIgnoreCase))
                atendidas++;
            else if (actual.estado.Equals("Devuelto",
                StringComparison.OrdinalIgnoreCase))
                devueltas++;

            actual = actual.siguiente;
        }

        Console.WriteLine("Total de solicitudes: " + total);
        Console.WriteLine("Pendientes: " + pendientes);
        Console.WriteLine("Atendidas: " + atendidas);
        Console.WriteLine("Devueltas: " + devueltas);
        Pausar();
    }

    public void Ejecutar()
    {
        int opcion = 0;

        do
        {
            Console.Clear();
            Console.WriteLine("SISTEMA DE DATA CENTER");
            Console.WriteLine("1. Registrar solicitud");
            Console.WriteLine("2. Buscar solicitud");
            Console.WriteLine("3. Modificar solicitud");
            Console.WriteLine("4. Eliminar solicitud");
            Console.WriteLine("5. Mostrar solicitudes");
            Console.WriteLine("6. Buscar por nombre");
            Console.WriteLine("7. Mostrar solicitudes pendientes");
            Console.WriteLine("8. Mostrar estadisticas");
            Console.WriteLine("9. Salir");
            Console.Write("Seleccione una opcion: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("Ingrese un numero valido.");
                Pausar();
                continue;
            }

            switch (opcion)
            {
                case 1: Registrar(); break;
                case 2: BuscarSolicitud(); break;
                case 3: Modificar(); break;
                case 4: Eliminar(); break;
                case 5: MostrarSolicitudes(); break;
                case 6: BuscarNombre(); break;
                case 7: MostrarPendientes(); break;
                case 8: Estadisticas(); break;
                case 9: Console.WriteLine("Saliendo del sistema..."); break;
                default:
                    Console.WriteLine("Opcion no valida.");
                    Pausar();
                    break;
            }
        } while (opcion != 9);
    }
}

class Program
{
    static void Main()
    {
        GestorBiblioteca gestor = new GestorBiblioteca();
        gestor.Ejecutar();
    }
}
