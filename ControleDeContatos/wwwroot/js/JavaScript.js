

alert("Entrou");

    async function PegarJson(){
        let response = await fetch("http://localhost:5149/api/ContatoApi/api/TodosContatos");
    let data = await response.json();
    return data 
        }

    PegarJson().then(function(data)
    {

        console.log(data)
                data.forEach(element => {
        document.getElementById('json').innerHTML += element.nome;
    document.getElementById('json').innerHTML += "<br>"
                });
            }
        )
