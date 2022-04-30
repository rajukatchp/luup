import { Component, OnInit } from '@angular/core';
// import * as monaco from "monaco-editor";
// import * as markdownit from "markdown-it";
// import * as ACDesigner from "adaptivecards-designer";

@Component({
  selector: 'app-create-card',
  templateUrl: './create-card.component.html',
  styleUrls: ['./create-card.component.scss']
})
export class CreateCardComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  //   ACDesigner.CardDesigner.onProcessMarkdown = (text, result) => {
  //     result.outputHtml = new markdownit().render(text);
  //     result.didProcess = true;
  // }
  // let hostContainers:  any = [];
  // let designer = new ACDesigner.CardDesigner(hostContainers);
  // // @ts-ignore
  // designer.attachTo(document.getElementById("designerRootHost"));
	// designer.monacoModuleLoaded(monaco);
  }

}
