import { Component, OnDestroy, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Student } from 'src/app/models/Student';
import { Teacher } from 'src/app/models/Teacher';
import { StudentService } from 'src/app/services/Student.service';
import { TeacherService } from 'src/app/services/Teacher.service';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit, OnDestroy
{
  private unsubscriber = new Subject();

  public modalRef: BsModalRef;
  public studentForm: FormGroup;
  public studentSelected: Student;
  public sampleText: string;
  public teachers: Teacher[];
  public students: Student[];
  public student: Student;
  public deleteStudentMessage: string;
  public modeSave = 'post';
  public title = 'Alunos';

  constructor(private studentService: StudentService, private route: ActivatedRoute, private teacherService: TeacherService,
              private fb: FormBuilder, private modalService: BsModalService, private toastr: ToastrService,
              private spinner: NgxSpinnerService)
  {
    this.createForm();
  }

  ngOnInit(): void
  {
    this.getStudents();
  }

  selectStudent(student: Student): void
  {
    this.modeSave = 'put';
    this.studentSelected = student;
    this.studentForm.patchValue(student);
  }

  unselectStudent(): void
  {
    this.studentSelected = null;
  }

  getTeachers(template: TemplateRef<any>, id: number): void
  {
    this.spinner.show();

    this.teacherService.getByStudentId(id)
      .pipe(takeUntil(this.unsubscriber))
      .subscribe((teachers: Teacher[]) => {
        this.teachers = teachers;
        this.modalRef = this.modalService.show(template);
      },
      (error: any) => {
        this.toastr.error(`erro: ${error}`);
        console.error(error);
      },
      () => this.spinner.hide());
  }

  createForm(): void
  {
    this.studentForm = this.fb.group({
      id: [0],
      nome: ['', Validators.required],
      telefone: ['', Validators.required]
    });
  }

  saveStudent(): void
  {
    if (this.studentForm.valid)
    {
      this.spinner.show();

      if (this.modeSave === 'post')
      {
        this.student = {...this.studentForm.value};
      }
      else
      {
        this.student = {id: this.studentSelected.id, ...this.studentForm.value};
      }

      this.studentService[this.modeSave](this.student)
        .pipe(takeUntil(this.unsubscriber))
        .subscribe(() => {
          this.getStudents();
          this.toastr.success('Aluno salvo com sucesso!');
        },
        (error: any) => {
          this.toastr.error(`Erro ao salvar aluno!`);
          console.error(error);
        },
        () => this.spinner.hide());
    }
  }

  getStudents(): void {
    const id = +this.route.snapshot.paramMap.get('id');

    this.spinner.show();

    this.studentService.getAll()
      .pipe(takeUntil(this.unsubscriber))
      .subscribe((students: Student[]) => {
        this.students = students;

        if (id > 0)
        {
          this.selectStudent(this.students.find(s => s.id === id));
        }

        this.toastr.success('Alunos carregados com sucesso!');
      },
      (error: any) => {
        this.toastr.error('Erro ao carregar os alunos!');
        console.error(error);
      },
      () => this.spinner.hide());
  }

  openModal(template: TemplateRef<any>, studentId: number): void
  {
    this.getTeachers(template, studentId);
  }

  closeModal(): void
  {
    this.modalRef.hide();
  }

  ngOnDestroy(): void
  {
    this.unsubscriber.next();
    this.unsubscriber.complete();
  }
}
