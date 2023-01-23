import { useFormik } from "formik";
import { useRef, useState } from "react";

export const RegistrarPelicula = ({setRegistrarPelicula}) => {

    const [foto, setFoto] = useState('https://png.pngtree.com/element_our/png_detail/20181227/movie-icon-which-is-designed-for-all-application-purpose-new-png_287896.jpg');
    const inputFile = useRef();

    const formulario = useFormik({
        initialValues: {
            id:0,
            titulo:' ',
            titulo_original:' ',
            anio:' ',
            duracion:' ',
            sinopsis:' ',
            portada:' ',
            estado:0
        },
        onSubmit: async(data) => {
            data.portada = foto;
            const d =  JSON.parse(sessionStorage.getItem('data'));
            try{
                const resultado = await fetch('http://localhost:5220/api/Pelicula/RegistrarPelicula', {
                    method: 'POST',
                    headers: {
                        'Authorization': `Bearer ${d.token}`,
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(data)
                });
    
                if(resultado.ok){
                    window.location.reload(false);
                }
            }catch(e){
                console.log(e);
            }  
        }
    });

    const cambiarFoto = async() => {
        const image = inputFile.current.files[0];
        const formData = new FormData();
        formData.append('image', image);

        await fetch('http://localhost:5220/api/Imagen/api/Imagen/SubirFoto', {
                method: 'POST',
                body: formData
            })
            .then(response => response.text())
            .then(response => {
                 console.log(response);
                 setFoto(response);
            });
    }


  return (
    <>
    <div className="window">
        <div className="window-body">
            <form onSubmit={formulario.handleSubmit} className='login' >
                <h1 className='text-center mb-3' >Registrar Pelicula</h1>

                <div className='fotoPerfil' >
                    <img src={foto} width='200px' />
                    <label htmlFor="inputfile"><i className="bi bi-cloud-arrow-up-fill"></i> Cambiar foto...</label>
                    <input type="file" ref={inputFile} onChange={cambiarFoto} id='inputfile' />
                </div>


                <div className="mb-3" >
                    <label htmlFor="titulo" className="form-label" >Titulo</label>
                    <input type="text" name="titulo" id="titulo" className="form-control"
                    onChange={formulario.handleChange}
                    value={formulario.values.titulo}
                    />
                </div>
                <div className="mb-3" >
                    <label htmlFor="titulo_original" className="form-label" >Titulo Original</label>
                    <input type="text" name="titulo_original" id="titulo_original" className="form-control"
                    onChange={formulario.handleChange}
                    value={formulario.values.titulo_original}
                    />
                </div>
                <div className="mb-3" >
                    <label htmlFor="anio" className="form-label" >Año</label>
                    <input type="text" name="anio" id="anio" className="form-control"
                    onChange={formulario.handleChange}
                    value={formulario.values.anio}
                    />
                </div>
                <div className="mb-3" >
                    <label htmlFor="duracion" className="form-label" >Duracion</label>
                    <input type="text" name="duracion" id="duracion" className="form-control"
                    onChange={formulario.handleChange}
                    value={formulario.values.duracion}
                    />
                </div>
                <div className="mb-3" >
                    <label htmlFor="sinopsis" className="form-label" >Sinopsis</label>
                    <textarea className="form-control" name="sinopsis" id="sinopsis"
                    onChange={formulario.handleChange}
                    value={formulario.values.sinopsis}
                    ></textarea>
                </div>
                <div className="d-flex justify-content-center " >
                    <button type="submit" className="btn btn-success me-3" >Guardar Cambios</button>
                    <button  type="buttom" className="btn btn-danger" onClick={() => {setRegistrarPelicula(false)}}>Cancelar</button>
                </div>

            </form>
        </div>
    </div>
    
    </>
  )
}
