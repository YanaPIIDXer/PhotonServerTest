resource "aws_instance" "photon_server" {
    ami           = "ami-09cf6a62116b95ed8"     // 英語版Editionだが止む無し
    instance_type = "t3.large"        // ランニングコストかかるけど、t2.microだと話にならない
    associate_public_ip_address = true
    key_name = "key"

    credit_specification {
        cpu_credits = "unlimited"
    }

    subnet_id = aws_subnet.public.id
    security_groups = [aws_security_group.server_sg.id]

    tags = {
        Name = "PhotonServer"
    }
}

resource "aws_security_group" "server_sg" {
    name = "PhotonServer"
    description = "For PhotonServer"
    vpc_id = aws_vpc.main.id
    
    ingress {
        from_port        = 22
        to_port          = 22
        protocol         = "TCP"
        cidr_blocks      = ["0.0.0.0/0"]
    }
    ingress {
        from_port        = 3389
        to_port          = 3389
        protocol         = "TCP"
        cidr_blocks      = ["0.0.0.0/0"]
    }

    ingress {
        from_port        = 4530
        to_port          = 4530
        protocol         = "TCP"
        cidr_blocks      = ["0.0.0.0/0"]
    }

    egress {
        from_port        = 0
        to_port          = 0
        protocol         = "-1"
        cidr_blocks      = ["0.0.0.0/0"]
        ipv6_cidr_blocks = ["::/0"]
    }
}
