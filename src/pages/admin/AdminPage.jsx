import { useEffect, useState } from "react";
import { EditarUsuario } from "./components/EditarUsuario";
import { RegistrarPelicula } from "./components/RegistrarPelicula";

function AdminPage() {
    const [usuarios, setUsuarios] = useState([]);
    const [peliculas, setPeliculas] = useState([]);
    const [userSelect, setUserSelect] = useState({});
    const [editarUsuario, setEditarUsuario] = useState(false);
    const [registrarPelicula, setRegistrarPelicula] = useState(false);

    const ListarUsuarios = async() => {
        const data =  JSON.parse(sessionStorage.getItem('data'));
        try{
            const response = await fetch('http://localhost:5220/api/Usuario/ListarUsuarios',{
                method:'GET',
                headers: {
                    'Authorization': `Bearer ${data.token}`,
                    'Content-Type': 'application/json'
                }
            });
            if(response.ok){
                const jsonResponse = await response.json();
                setUsuarios( jsonResponse );
            }
        }catch(error){
            console.log(error)
        }
    }

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
            }
        }catch(error){
            console.log(error)
        }
    }

    const EliminarUsuario = async(id) => {
        const data =  JSON.parse(sessionStorage.getItem('data'));
        try{
            const response = await fetch('http://localhost:5220/api/Usuario/EliminarUsuario?id='+id,{
                method:'DELETE',
                headers: {
                    'Authorization': `Bearer ${data.token}`,
                    'Content-Type': 'application/json'
                }
            });
            if(response.ok){
                ListarUsuarios();                
            }
        }catch(error){
            console.log(error)
        }
    }

    const EliminarPelicula = async(id) => {
        const data =  JSON.parse(sessionStorage.getItem('data'));
        try{
            const response = await fetch('http://localhost:5220/api/Pelicula/EliminarPelicula?id='+id,{
                method:'DELETE',
                headers: {
                    'Authorization': `Bearer ${data.token}`,
                    'Content-Type': 'application/json'
                }
            });
            if(response.ok){
                ListarPeliculas();                
            }
        }catch(error){
            console.log(error)
        }
    }


    useEffect(() => {
       ListarUsuarios();
       ListarPeliculas();
    }, []);

    console.log(peliculas);
    return (
        <>
            <div className="container">
                <div className="row">
                    <div className="col">
                        <h2 className="my-3" >Administracion</h2>
                        <p> <span className="fw-bold" >Nota: </span> Esta pagina solo esta permitida a los administradores del sitio web</p>
                    </div>
                </div>
                <div className="row">
                    <div className="col">
                        <div className="accordion" id="accordionExample">
                            <div className="accordion-item">
                                <h2 className="accordion-header" id="headingOne">
                                <button className="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                    Usuarios
                                </button>
                                </h2>
                                <div id="collapseOne" className="accordion-collapse collapse show" aria-labelledby="headingOne" data-bs-parent="#accordionExample">
                                <div className="accordion-body">

                                <table className="table table-striped">
                                    <thead>
                                        <tr>
                                            <th>Nombre</th>
                                            <th>Apellido</th>
                                            <th>Foto</th>
                                            <th>Usuario</th>
                                            <th>Mas</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {usuarios.map( e => (
                                            <tr key={e.id} >
                                               <td> {e.nombre} </td> 
                                               <td> {e.apellido} </td> 
                                               <td>  <img src={e.foto} height='100px' />  </td> 
                                               <td> {e.username} </td> 
                                               <td> 
                                                    <div className="d-flex flex-column">
                                                        <button className="btn btn-success mb-3" onClick={() => {setUserSelect(e); setEditarUsuario(true)}}> Editar </button>
                                                        <button className="btn btn-danger" onClick={() => {EliminarUsuario(e.id)}} > Eliminar </button> 
                                                    </div>
                                               </td>
                                            </tr>
                                        ))}

                                    </tbody>
                                </table>
                                    
                                </div>
                                </div>
                            </div>
                            <div className="accordion-item">
                                <h2 className="accordion-header" id="headingTwo">
                                <button className="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                    Peliculas
                                </button>
                                </h2>
                                <div id="collapseTwo" className="accordion-collapse collapse" aria-labelledby="headingTwo" data-bs-parent="#accordionExample">
                                    <div className="accordion-body">
                                        <span className="text-primary pointer"  onClick={() => {setRegistrarPelicula(true)}}> Registrar nueva pelicula </span>

                                        <table className="table table-striped">
                                            <thead>
                                                <tr>
                                                    <th>Titulo</th>
                                                    <th>Año</th>
                                                    <th>Sinopsis</th>
                                                    <th>Portada</th>
                                                    <th>Mas</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                {peliculas.map( e => (
                                                    <tr key={e.id} >
                                                    <td> {e.titulo} </td> 
                                                    <td> {e.anio} </td> 
                                                    <td> {e.sinopsis} </td> 
                                                    <td>  <img src={e.portada} height='100px' />  </td> 
                                                    <td> 
                                                            <div className="d-flex flex-column">
                                                                {/* <button className="btn btn-success mb-3"> Editar </button> */}
                                                                <button className="btn btn-danger" onClick={() => {EliminarPelicula(e.id)}} > Eliminar </button> 
                                                            </div>
                                                    </td>
                                                    </tr>
                                                ))}

                                            </tbody>
                                        </table>
                                                                              
                                    </div>
                                </div>
                            </div>
                            {/* <div className="accordion-item">
                                <h2 className="accordion-header" id="headingThree">
                                <button className="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                                    Accordion Item #3
                                </button>
                                </h2>
                                <div id="collapseThree" className="accordion-collapse collapse" aria-labelledby="headingThree" data-bs-parent="#accordionExample">
                                <div className="accordion-body">
                                    <strong>This is the third item's accordion body.</strong> It is hidden by default, until the collapse plugin adds the appropriate classNamees that we use to style each element. These classNamees control the overall appearance, as well as the showing and hiding via CSS transitions. You can modify any of this with custom CSS or overriding our default variables. It's also worth noting that just about any HTML can go within the <code>.accordion-body</code>, though the transition does limit overflow.
                                </div>
                                </div>
                            </div> */}
                        </div>
                    </div>
                </div>
            </div>

            {/* MODALES */}
            { editarUsuario&&
                <EditarUsuario usuario={userSelect} setEditarUsuario={setEditarUsuario} />
            }
            { registrarPelicula&&
                <RegistrarPelicula setRegistrarPelicula={setRegistrarPelicula} />
            }

        </>
     );
}

export default AdminPage;