<h1>Sistema de gestão de dados universitários</h1>
<br>


<p>Este projeto aborda o desenvolvimento de um sistema para a gestão de dados de uma universidade. O objetivo foi modelar o sistema utilizando diagramas UML, começando pelo diagrama de casos de uso, que representará o cadastro e as interações de diferentes tipos de pessoas com o sistema. Cada caso de uso foi detalhado com a descrição de seu cenário principal, dois cenários alternativos, além de pré-condições e pós-condições. Também foi desenvolvido um diagrama de classes, alinhado à proposta do projeto, contendo pelo menos cinco classes que representem os principais elementos do sistema.</p>
<br>
<h2>Diagrama de caso de uso</h2>
<p>No diagrama de caso de uso abaixo é possível observar a existência de cinco casos de uso, sendo eles: Cadastro de Pessoa Física, Cadastro de Pessoa Jurídica, Cadastro de Professores, Cadastro de Fornecedores e Cadastro de Alunos. Além disso, têm-se também quatro atores, sendo eles: Usuário, que é o ator principal pois interage com o sistema ativamente, Aluno, Professor e Fornecedor, que se relacionam com os casos de uso de forma passiva.</p>

![image](https://github.com/user-attachments/assets/17a96416-d641-45fa-a40e-80cc1c5c74fd)


<h3>Descrição dos cenários de casos de uso</h3>

<ol>
<li><strong>Cadastro de usuário</strong></li>
<br>
<em>Cenário principal:</em><br>
Realizar o cadastro de um usuário com sucesso. Os dados coletados nesse cadastro serão: nome, e-mail, senha e confirmação de senha.<br>
<br>
<em>Cenários alternativos:</em><br>
<ul>
  <li>Cadastrar um usuário com senhas divergentes;</li>
  <li>Cadastrar um usuário informando um e-mail inválido.</li>
</ul>
<br>
<em>Pré-condições:</em><br>
<ul>
  <li>O sistema deve estar conectado ao banco de dados para validar e registrar as informações;</li>
  <li>Os campos devem estar devidamente configurados para validação (obrigatoriedade de preenchimento)</li>
</ul>
<br>
<em>Pós-condições:</em><br>
<ul>
  <li>Os dados do usuário devem estar armazenados no banco de dados, com todas as informações cadastradas corretamente;</li>
  <li>O sistema deve confirmar ao usuário que o cadastro foi realizado com sucesso;</li>
  <li>O sistema deve garantir que não haja duplicidade de e-mails;</li>
  <li>Em caso de erro, o sistema deve exibir mensagens claras ao usuário indicando a falha e os passos para correção.</li>
</ul>
<br> 

  
<li><strong>Login</strong></li>
<br>
<em>Cenário principal:</em><br>
Realizar o login com sucesso: O sistema deverá autenticar usuário e verificar autorização conforme registro no banco de dados.<br>
<br>
<em>Cenários alternativos:</em><br>
<ul>
  <li>Efetuar login sem informar o e-mail ou senha;</li>
  <li>Efetuar login com senha incorreta.</li>
</ul>
<br>
<em>Pré-condições:</em><br>
<ul>
  <li>O sistema deve estar conectado ao banco de dados para validar e registrar as informações;</li>
  <li>O usuário deve estar cadastrado para poder acessar ao sistema.</li>
</ul>
<br>
<em>Pós-condições:</em><br>
<p>Em caso de erro, o sistema deve exibir mensagens claras ao usuário indicando a falha e os passos para correção.</p>
<br>

  
<li><strong>Cadastro de pessoa física</strong></li>
<br>
<em>Cenário principal:</em><br>
Realizar o cadastro de uma pessoa física com sucesso. Os dados coletados nesse cadastro serão: nome, CPF, data de nascimento, e-mail, telefone e endereço.<br>
<br>
<em>Cenários alternativos:</em><br>
<ul>
  <li>Cadastrar uma pessoa física sem informar o CPF;</li>
  <li>Cadastrar uma pessoa física com CPF já cadastrado no sistema.</li>
</ul>
<br>
<em>Pré-condições:</em><br>
<ul>
  <li>O usuário deve estar autenticado no sistema com um perfil que permita realizar cadastro;</li>
  <li>O sistema deve estar conectado ao banco de dados para validar e registrar as informações;</li>
  <li>Os campos devem estar devidamente configurados para validação (obrigatoriedade de preenchimento)</li>
</ul>
<br>
<em>Pós-condições:</em><br>
<ul>
  <li>Os dados da pessoa física devem estar armazenados no banco de dados, com todas as informações cadastradas corretamente;</li>
  <li>O sistema deve confirmar ao usuário que o cadastro foi realizado com sucesso;</li>
  <li>O novo cadastro deve estar disponível para consulta no sistema, podendo ser recuperado por meio de filtros como CPF, nome ou e-mail;</li>
  <li>O sistema deve garantir que não haja duplicidade de CPF ou e-mail para o cadastro de pessoas físicas;</li>
  <li>Em caso de erro, o sistema deve exibir mensagens claras ao usuário indicando a falha e os passos para correção.</li>
</ul>
<br>


<li><strong>Cadastro de aluno</strong></li>
<br>
<em>Cenário principal:</em><br>
Realizar o cadastro de um aluno com sucesso. Os dados pessoais do aluno serão herdados do cadastro de pessoa física (feito previamente). Além dos dados contidos no cadastro de pessoa física, serão coletados os seguintes dados: número de matrícula, nome do curso e disciplinas.<br>
<br>
<em>Cenários alternativos:</em><br>
<ul>
  <li>Cadastrar aluno sem informar o número de matrícula;</li>
  <li>Cadastrar aluno com número de matrícula já utilizada no sistema.</li>
</ul>
<br>
<em>Pré-condições:</em><br>
<ul>
  <li>O usuário deve estar autenticado no sistema com um perfil que permita realizar matrícula;</li>
  <li>O sistema deve estar conectado ao banco de dados para validar e registrar as informações;</li>
  <li>O campo número de matrícula deve estar devidamente configurado para validação (obrigatoriedade de preenchimento)</li>
</ul>
<br>
<em>Pós-condições:</em><br>
<ul>
  <li>Os dados do aluno devem estar armazenados no banco de dados, com todas as informações cadastradas corretamente;</li>
  <li>O sistema deve confirmar ao usuário que o aluno foi cadastrado com sucesso;</li>
  <li>O novo cadastro deve estar disponível para consulta no sistema, podendo ser recuperado por meio de filtros como número de matrícula;</li>
  <li>O sistema deve garantir que não haja duplicidade de número de matrícula;</li>
  <li>Em caso de erro, o sistema deve exibir mensagens claras ao usuário indicando a falha e os passos para correção.</li>
</ul>
<br>


<li><strong>Cadastro de professor</strong></li>
<br>
<em>Cenário principal:</em><br>
Realizar o cadastro de um professor com sucesso. Assim como no caso dos alunos, os dados pessoais do professor serão herdados do cadastro de pessoa física (feito previamente). Além dos dados contidos no cadastro de pessoa física, serão coletados os seguintes dados: número de matrícula, cursos e disciplinas que leciona.<br>
<br>
<em>Cenários alternativos:</em><br>
<ul>
  <li>Cadastrar professor sem informar o número de matrícula;</li>
  <li>Cadastrar professor com número de matrícula já utilizada no sistema.</li>
</ul>
<br>
<em>Pré-condições:</em><br>
<ul>
  <li>O usuário deve estar autenticado no sistema com um perfil que permita realizar cadastro;</li>
  <li>O sistema deve estar conectado ao banco de dados para validar e registrar as informações;</li>
  <li>O campo número de matrícula deve estar devidamente configurado para validação (obrigatoriedade de preenchimento)</li>
</ul>
<br>
<em>Pós-condições:</em><br>
<ul>
  <li>Os dados do professor devem estar armazenados no banco de dados, com todas as informações cadastradas corretamente;</li>
  <li>O sistema deve confirmar ao usuário que o professor foi cadastrado com sucesso;</li>
  <li>O novo cadastro deve estar disponível para consulta no sistema, podendo ser recuperado por meio de filtros como número de matrícula;</li>
  <li>O sistema deve garantir que não haja duplicidade de número de matrícula;</li>
  <li>Em caso de erro, o sistema deve exibir mensagens claras ao usuário indicando a falha e os passos para correção.</li>
</ul>
<br>


<li><strong>Cadastro de empresa</strong></li>
<br>
<em>Cenário principal:</em><br>
Realizar o cadastro de uma empresa com sucesso. Serão coletados os seguintes dados: nome, número do CNPJ, e-mail, telefone, endereço.<br>
<br>
<em>Cenários alternativos:</em><br>
<ul>
  <li>Cadastrar empresa com CNPJ já utilizado no sistema.</li>
  <li>Cadastrar empresa com e-mail já utilizado no sistema.</li>
</ul>
<br>
<em>Pré-condições:</em><br>
<ul>
  <li>O usuário deve estar autenticado no sistema com um perfil que permita realizar cadastro;</li>
  <li>O sistema deve estar conectado ao banco de dados para validar e registrar as informações;</li>
  <li>Os campos CNPJ e e-mail devem estar devidamente configurados para validação (obrigatoriedade de preenchimento)</li>
</ul>
<br>
<em>Pós-condições:</em><br>
<ul>
  <li>Os dados da empresa devem estar armazenados no banco de dados, com todas as informações cadastradas corretamente;</li>
  <li>O sistema deve confirmar ao usuário que a empresa foi cadastrada com sucesso;</li>
  <li>O novo cadastro deve estar disponível para consulta no sistema, podendo ser recuperado por meio de filtros como CNPJ;</li>
  <li>O sistema deve garantir que não haja duplicidade de CNPJ;</li>
  <li>Em caso de erro, o sistema deve exibir mensagens claras ao usuário indicando a falha e os passos para correção.</li>
</ul>
<br>


<li><strong>Cadastro de fornecedor</strong></li>
<br>
<em>Cenário principal:</em><br>
Realizar o cadastro de um fornecedor com sucesso. Os dados básicos da empresa serão herdados do cadastro de pessoa jurídica (feito previamente). Além dos dados contidos no cadastro de pessoa jurídica, serão coletados os seguintes dados: número do contrato, periodicidade do serviço, valor do serviço, data pagamento, vencimento do contrato.<br>
<br>
<em>Cenários alternativos:</em><br>
<ul>
  <li>Cadastrar fornecedor com CNPJ inválido/ainda não cadastrado.</li>
  <li>Cadastrar fornecedor sem informar o número do contrato.</li>
</ul>
<br>
<em>Pré-condições:</em><br>
<ul>
  <li>O usuário deve estar autenticado no sistema com um perfil que permita realizar cadastro;</li>
  <li>O sistema deve estar conectado ao banco de dados para validar e registrar as informações;</li>
  <li>O campo número de contrato deve estar devidamente configurado para validação (obrigatoriedade de preenchimento).</li>
</ul>
<br>
<em>Pós-condições:</em><br>
<ul>
  <li>Os dados do fornecedor devem estar armazenados no banco de dados, com todas as informações cadastradas corretamente;</li>
  <li>O sistema deve confirmar ao usuário que o fornecedor foi cadastrado com sucesso;</li>
  <li>O novo cadastro deve estar disponível para consulta no sistema, podendo ser recuperado por meio de filtros como número do contrato;</li>
  <li>O sistema deve garantir que não haja duplicidade de fornecedores;</li>
  <li>Em caso de erro, o sistema deve exibir mensagens claras ao usuário indicando a falha e os passos para correção.</li>
</ul>
<br>

<br>
<h2>Diagrama de classe</h2>
<p>As classes principais foram organizadas com base nos requisitos dos casos de uso fornecidos anteriormente, estruturando-se para promover reutilização e extensibilidade por meio de herança, agregação e composição, quando necessário. </p>
<p>No Diagrama de Classes UML, a herança é destacada com Aluno e Professor, que herdam de PessoaFisica, e Fornecedor, que herda de Empresa. </p>
<p>Já a composição é representada pelas listas de disciplinas e cursos presentes em Aluno e Professor. A agregação é evidenciada pela relação entre as classes Empresa e PessoaFisica com a classe Endereço, onde essa última atua como parte das classes anteriores.</p>

![image](https://github.com/user-attachments/assets/7c4ad6b4-945e-4302-9c96-d553a81bf9cf)


