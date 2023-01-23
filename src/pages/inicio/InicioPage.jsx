import { useFormik } from "formik";
import { useEffect, useState } from "react";

export const InicioPage = () => {
  const [peliculas, setPeliculas] = useState([]);
  const [showPeliculas, setShowPeliculas] = useState([]);
  const busqueda = useFormik({
    initialValues: {
      nombre:''
    },
    onSubmit: (v) => {
      console.log(peliculas);
      const newPelis = peliculas.filter(i => i.titulo.toLocaleLowerCase().includes(v.nombre.trim().toLocaleLowerCase()))
      setShowPeliculas(newPelis);
    }
  });

  const ListarPeliculas = async() => {
      const data =  JSON.parse(sessionStorage.getItem('data'));
      try{
          const response = await fetch('http://localhost:5220/api/Pelicula/ListarPeliculas',{
              method:'GET',
              headers: {
                  'Authorization': `Bearer ${data.token}`,
                  'Content-Type': 'application/json'
              }
          });
          if(response.ok){
              const jsonResponse = await response.json();
              setPeliculas( jsonResponse );
              setShowPeliculas( jsonResponse );
              console.log(jsonResponse);
          }
      }catch(error){
          console.log(error)
      }
  }

  useEffect(() => {
    ListarPeliculas();
  }, []);


 


  return (
    <>

        <div className="container">
          <h1 className="my-3" >Inicio Page</h1>

          <form className="my-3" onSubmit={busqueda.handleSubmit}>
            <label htmlFor="buscar" className="me-3 " >Buscar pelicula</label>
            <div className="d-flex">
              <input type="search" id="nombre" name="nombre" className="form-control"
              onChange={busqueda.handleChange}
              value={busqueda.values.nombre}
              />
              <button type="submit" className="btn btn-primary ms-3"> Buscar </button>
            </div>
          </form>



          <div className="row">
            {showPeliculas.map(e=>(
              <div key={e.id} className="col-3">
                <div className="card">
                  <img src={e.portada} className="card-img-top"/>
                  <div className="card-body">
                    <h5 className="card-title">{e.titulo}</h5>
                    <p className="card-text">{e.sinopsis}</p>
                    <div className="btn btn-secundary ">Ver</div>
                  </div>
                </div>
              </div>
            ))}
          </div>
        </div>



    </>
  )
}
