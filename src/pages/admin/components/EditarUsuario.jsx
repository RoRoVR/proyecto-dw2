import { useFormik } from "formik";
import { useRef, useState } from "react";

export const EditarUsuario = ({usuario, setEditarUsuario}) => {
    const [foto, setFoto] = useState(usuario.foto);
    const inputFile = useRef();

    const formulario = useFormik({
        initialValues: {
            id:usuario.id,
            nombre:usuario.nombre,
            apellido:usuario.apellido,
            descripcion:usuario.descripcion,
            username:usuario.username,
            password:usuario.password,
            foto:usuario.foto
        },
        onSubmit: async(data) => {
            let newData = {...data, foto};
            console.log(newData);
            try{
                const resultado = await fetch('http://localhost:5220/api/Usuario/Registar_Atualizar_Usuario', {
                    method: 'POST',
                    headers: {
                    'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(newData)
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
    <div className="window p-3 "> 
        <div className="window-body">
            <form onSubmit={formulario.handleSubmit} className='login' >
                <h1 className='text-center mb-3' >Editar Usuario</h1>

                <div className='fotoPerfil' >
                    <img src={foto} width='200px' />
                    <label htmlFor="inputfile"><i className="bi bi-cloud-arrow-up-fill"></i> Cambiar foto...</label>
                    <input type="file" ref={inputFile} onChange={cambiarFoto} id='inputfile' />
                </div>

                <div className="mb-3" >
                    <label htmlFor="nombre" className="form-label" >Nombre</label>
                    <input type="text" name="nombre" id="nombre" className="form-control"
                    onChange={formulario.handleChange}
                    value={formulario.values.nombre}
                    />
                </div>
                <div className="mb-3" >
                    <label htmlFor="apellido" className="form-label" >Apellido</label>
                    <input type="text" name="apellido" id="apellido" className="form-control"
                    onChange={formulario.handleChange}
                    value={formulario.values.apellido}
                    />
                </div>
                <div className="mb-3" >
                    <label htmlFor="username" className="form-label" >Usuario</label>
                    <input type="text" name="username" id="username" className="form-control"
                    onChange={formulario.handleChange}
                    value={formulario.values.username}
                    />
                </div>
                <div className="mb-3" >
                    <label htmlFor="password" className="form-label" >Contraseña</label>
                    <input type="password" name="password" id="password" className="form-control"
                    onChange={formulario.handleChange}
                    value={formulario.values.password}
                    />
                </div>
                <div className="d-flex justify-content-center " >
                    <button type="submit" className="btn btn-success me-3" >Guardar Cambios</button>
                    <button  type="buttom" className="btn btn-danger" onClick={() => {setEditarUsuario(false)}} >Cancelar</button>
                </div>

            </form>
        </div>
    </div>
  )
}
