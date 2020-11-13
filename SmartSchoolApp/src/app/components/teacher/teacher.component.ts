import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Util } from 'src/app/helpers/Util';
import { Discipline } from 'src/app/models/Discipline';
import { Teacher } from 'src/app/models/Teacher';
import { TeacherService } from 'src/app/services/Teacher.service';

@Component({
  selector: 'app-teacher',
  templateUrl: './teacher.component.html',
  styleUrls: ['./teacher.component.css']
})
export class TeacherComponent implements OnInit, OnDestroy {
  private unsubscriber = new Subject();

  public title = 'Professores';
  public teachers: Teacher[];

  constructor(private teacherService: TeacherService, private route: ActivatedRoute, private toastr: ToastrService,
              private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.getTeachers();
  }

  getTeachers(): void {
    this.spinner.show();

    this.teacherService.getAll()
      .pipe(takeUntil(this.unsubscriber))
      .subscribe((teachers: Teacher[]) => {
        this.teachers = teachers;
      },
      (errorResp: any) => {
        this.toastr.error(errorResp.error);
        console.error(errorResp);
        this.spinner.hide();
      },
      () => this.spinner.hide());
  }

  concatDisciplines(disciplines: Discipline[]): string {
    return Util.concatArray(disciplines);
  }

  ngOnDestroy(): void {
    this.unsubscriber.next();
    this.unsubscriber.complete();
  }
}
