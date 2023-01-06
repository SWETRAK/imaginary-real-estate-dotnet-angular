import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IreContactPageComponent } from './ire-contact-page.component';

describe('IreContactPageComponent', () => {
  let component: IreContactPageComponent;
  let fixture: ComponentFixture<IreContactPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IreContactPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IreContactPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
